using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Dtos.Facebook;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.DTOs.Facebook;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.FacebookLogin;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.GoogleLogin;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.InternalLogin;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.RefreshToken;
using CleanArchitectureTemplate.Domain.Common;
using CleanArchitectureTemplate.Persistence.Identity;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace CleanArchitectureTemplate.Persistence.Services.Auth.Identity;

public class IdentityAuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;
    private readonly UserManager<AppUser> _userManager;
    public readonly SignInManager<AppUser> _signInManager;

    public IdentityAuthenticationService(IConfiguration configuration, ITokenService tokenService, IUserService userService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _configuration = configuration;
        _tokenService = tokenService;
        _userService = userService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    #region Internal Authentication

    /// <inheritdoc />
    public async Task<TokenDTO?> InternalLoginAsync(InternalLoginCommandRequest model)
    {
        AppUser? user = null;

        if (!string.IsNullOrEmpty(model.UserName))
        {
            user = await _userManager.FindByNameAsync(model.UserName);
        }
        else if (!string.IsNullOrEmpty(model.Email))
        {
            user = await _userManager.FindByEmailAsync(model.Email);
        }

        BusinessRules.Run(("AUT853660", BusinessRules.CheckEntityNull(user)));

        var result = await _signInManager.CheckPasswordSignInAsync(user!, model.Password, false);

        if (result.Succeeded)
        {
            var tokenDto = _tokenService.GenerateToken(user!.Id);
            await _userService.UpdateRefreshTokenAsync(user!.Id, tokenDto.RefreshToken!, tokenDto.ExpirationTime);
            return tokenDto;
        }
        else
        {
            throw new Exception("Invalid password");
        }
    }

    #endregion Internal Authentication

    #region External Authentication

    /// <inheritdoc />
    public async Task<TokenDTO?> FacebookLoginAsync(FacebookLoginCommandRequest model)
    {
        // Validate the Facebook access token
        var appId = _configuration["OAuth:Facebook:AppId"];
        var appSecret = _configuration["OAuth:Facebook:AppSecret"];

        if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(appSecret))
        {
            throw new InvalidOperationException("Facebook AppId or AppSecret is not configured.");
        }

        var tokenValidationUrl = $"https://graph.facebook.com/debug_token?input_token={model.AccessToken}&access_token={appId}|{appSecret}";

        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(tokenValidationUrl);
        response.EnsureSuccessStatusCode();

        var validationResult = await response.Content.ReadFromJsonAsync<FacebookTokenValidationResultDTO>();
        if (validationResult?.Data == null || !validationResult.Data.IsValid)
        {
            throw new InvalidOperationException("Invalid Facebook access token.");
        }

        // Fetch user information from Facebook Graph API
        var userInfoUrl = $"https://graph.facebook.com/me?fields=id,email,first_name,last_name&access_token={model.AccessToken}";
        var userInfoResponse = await httpClient.GetAsync(userInfoUrl);
        userInfoResponse.EnsureSuccessStatusCode();

        var userInfo = await userInfoResponse.Content.ReadFromJsonAsync<FacebookUserInfoDTO>();
        if (userInfo == null)
        {
            throw new InvalidOperationException("Failed to fetch user information from Facebook.");
        }

        // Prepare UserLoginInfo for external login
        var info = new UserLoginInfo(model.Provider, userInfo.Id, model.Provider);

        // Attempt to find the user via external login
        var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        if (user == null)
        {
            // Attempt to find the user by email
            user = await _userManager.FindByEmailAsync(userInfo.Email);
            if (user == null)
            {
                // Create a new user if not found
                user = new AppUser
                {
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    EmailConfirmed = true,
                    FullName = $"{userInfo.FirstName} {userInfo.LastName}"
                };

                var creationResult = await _userManager.CreateAsync(user);
                if (!creationResult.Succeeded)
                {
                    throw new Exception("Failed to create user: " + string.Join(", ", creationResult.Errors.Select(e => e.Description)));
                }
            }

            // Link external login to the user
            var loginResult = await _userManager.AddLoginAsync(user, info);
            if (!loginResult.Succeeded)
            {
                throw new Exception("Failed to add external login: " + string.Join(", ", loginResult.Errors.Select(e => e.Description)));
            }
        }

        // Generate JWT token
        var tokenDto = _tokenService.GenerateToken(user.Id);
        await _userService.UpdateRefreshTokenAsync(user.Id, tokenDto.RefreshToken!, tokenDto.ExpirationTime);
        return tokenDto;
    }

    /// <inheritdoc />
    public async Task<TokenDTO?> GoogleLoginAsync(GoogleLoginCommandRequest model)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = [_configuration["OAuth:Google:ClientId"]]
        };

        GoogleJsonWebSignature.Payload payload;
        try
        {
            payload = await GoogleJsonWebSignature.ValidateAsync(model.IdToken, settings);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Invalid Google ID token.", ex);
        }

        //  This model is used to store the user's information to the AspNetUserLogins table
        var info = new UserLoginInfo(model.Provider, payload.Subject, model.Provider);
        var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        // If the user does not exist, create a new user
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new AppUser
                {
                    Email = payload.Email,
                    UserName = payload.Email,
                    EmailConfirmed = true,
                    FullName = $"{model.FirstName} {model.LastName}",
                };

                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create user");
                }
            }

            var loginResult = await _userManager.AddLoginAsync(user, info);
            if (!loginResult.Succeeded)
            {
                throw new Exception("Failed to add login");
            }
        }

        var tokenDto = _tokenService.GenerateToken(user.Id);
        await _userService.UpdateRefreshTokenAsync(user.Id, tokenDto.RefreshToken!, tokenDto.ExpirationTime);
        return tokenDto;
    }

    #endregion External Authentication

    #region Refresh Token

    /// <inheritdoc />
    public async Task<TokenDTO?> RefreshTokenAsync(RefreshTokenCommandRequest model)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == model.RefreshToken);
        BusinessRules.Run(("AUT913061", BusinessRules.CheckEntityNull(user)));

        if (user!.RefreshToken != model.RefreshToken)
        {
            throw new InvalidOperationException("Invalid refresh token.");
        }

        if (user.RefreshTokenExpiration < DateTime.UtcNow)
        {
            throw new InvalidOperationException("Refresh token has expired.");
        }

        var tokenDto = _tokenService.GenerateToken(user.Id);
        await _userService.UpdateRefreshTokenAsync(user.Id, tokenDto.RefreshToken!, tokenDto.ExpirationTime);

        return tokenDto;
    }

    #endregion Refresh Token
}