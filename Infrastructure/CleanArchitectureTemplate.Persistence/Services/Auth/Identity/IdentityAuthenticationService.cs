using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Dtos.Facebook;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.DTOs.Facebook;
using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.FacebookLogin;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.GoogleLogin;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.InternalLogin;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.RefreshToken;
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
    private readonly ICookieService _cookieService;
    private readonly IUserContextService _userContextService;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public IdentityAuthenticationService(IConfiguration configuration, ITokenService tokenService, ICookieService cookieService, IUserContextService userContextService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _configuration = configuration;
        _tokenService = tokenService;
        _cookieService = cookieService;
        _userContextService = userContextService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    #region Internal Authentication

    /// <inheritdoc />
    public async Task<TokenDto?> InternalLoginAsync(InternalLoginCommandRequest model)
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

        if (user == null)
            throw new UnauthorizedException("Invalid credentials.");

        var result = await _signInManager.CheckPasswordSignInAsync(user!, model.Password, false);

        if (result.Succeeded)
        {
            var roles = await _userManager.GetRolesAsync(user!);
            var tokenDto = _tokenService.GenerateToken(user.UserName!, roles);
            await UpdateRefreshTokenAsync(user!.Id, tokenDto.RefreshToken!, tokenDto.ExpirationTime);

            _cookieService.SetAuthCookies(tokenDto);
            return tokenDto;
        }
        else
        {
            throw new UnauthorizedException("Invalid credentials.");
        }
    }

    #endregion Internal Authentication

    #region External Authentication

    /// <inheritdoc />
    public async Task<TokenDto?> FacebookLoginAsync(FacebookLoginCommandRequest model)
    {
        // Validate the Facebook access token
        var appId = _configuration["OAuth:Facebook:AppId"]!;
        var appSecret = _configuration["OAuth:Facebook:AppSecret"]!;

        var tokenValidationUrl = $"https://graph.facebook.com/debug_token?input_token={model.AccessToken}&access_token={appId}|{appSecret}";

        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(tokenValidationUrl);
        response.EnsureSuccessStatusCode();

        var validationResult = await response.Content.ReadFromJsonAsync<FacebookTokenValidationResultDto>();
        if (validationResult?.Data == null || !validationResult.Data.IsValid)
        {
            throw new UnauthorizedException("Invalid Facebook access token.");
        }

        // Fetch user information from Facebook Graph API
        var userInfoUrl = $"https://graph.facebook.com/me?fields=id,email,first_name,last_name&access_token={model.AccessToken}";
        var userInfoResponse = await httpClient.GetAsync(userInfoUrl);
        userInfoResponse.EnsureSuccessStatusCode();

        var userInfo = await userInfoResponse.Content.ReadFromJsonAsync<FacebookUserInfoDto>()
            ?? throw new FailedDependencyException("Failed to fetch user information from Facebook.");

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
                    throw new BadRequestException(creationResult.Errors.Select(e => e.Description).FirstOrDefault());
                }
            }

            // Link external login to the user
            var loginResult = await _userManager.AddLoginAsync(user, info);
            if (!loginResult.Succeeded)
            {
                // Rollback user creation if failed to link external login
                await _userManager.DeleteAsync(user);
                throw new BadRequestException(loginResult.Errors.Select(e => e.Description).FirstOrDefault());
            }
        }

        // Generate JWT token
        var roles = await _userManager.GetRolesAsync(user);
        var tokenDto = _tokenService.GenerateToken(user.UserName!, roles);
        await UpdateRefreshTokenAsync(user.Id, tokenDto.RefreshToken!, tokenDto.ExpirationTime);
        _cookieService.SetAuthCookies(tokenDto);

        return tokenDto;
    }

    /// <inheritdoc />
    public async Task<TokenDto?> GoogleLoginAsync(GoogleLoginCommandRequest model)
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
        catch
        {
            throw new UnauthorizedException("Invalid Google ID token.");
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
                    throw new BadRequestException(result.Errors.Select(e => e.Description).FirstOrDefault());
                }
            }

            var loginResult = await _userManager.AddLoginAsync(user, info);
            if (!loginResult.Succeeded)
            {
                // Rollback user creation if failed to link external login
                await _userManager.DeleteAsync(user);

                throw new BadRequestException(loginResult.Errors.Select(e => e.Description).FirstOrDefault());
            }
        }

        var roles = await _userManager.GetRolesAsync(user);
        var tokenDto = _tokenService.GenerateToken(user.UserName!, roles);
        await UpdateRefreshTokenAsync(user.Id, tokenDto.RefreshToken!, tokenDto.ExpirationTime);
        _cookieService.SetAuthCookies(tokenDto);

        return tokenDto;
    }

    #endregion External Authentication

    #region Logout

    /// <inheritdoc />
    public async Task LogoutAsync()
    {
        await RevokeRefreshTokenAsync();
        _cookieService.ClearAuthCookies();

        // Commented out because it is not necessary to sign out the user because stateless JWT authentication is used
        // await _signInManager.SignOutAsync();
    }

    #endregion Logout

    #region Refresh Token

    /// <inheritdoc />
    public async Task<TokenDto?> RefreshTokenAsync(RefreshTokenCommandRequest model)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == model.RefreshToken);

        if (user == null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiration < DateTime.UtcNow)
        {
            throw new UnauthorizedException("Invalid refresh token.");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var tokenDto = _tokenService.GenerateToken(user.UserName!, roles);
        await UpdateRefreshTokenAsync(user.Id, tokenDto.RefreshToken!, tokenDto.ExpirationTime);
        _cookieService.SetAuthCookies(tokenDto);

        return tokenDto;
    }

    /// <inheritdoc />
    public async Task RevokeRefreshTokenAsync()
    {
        var userId = _userContextService.GetUserId()
            ?? throw new UnauthorizedException("User not authenticated.");

        var user = await _userManager.FindByIdAsync(userId.ToString())
            ?? throw new NotFoundException(nameof(AppUser), userId);

        if (string.IsNullOrEmpty(user.RefreshToken))
            throw new BadRequestException("No refresh token to revoke.");

        user.RefreshToken = null;
        user.RefreshTokenExpiration = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        _cookieService.ClearRefreshTokenCookie();
    }

    /// <summary>
    /// Updates refresh token for the user after login or token refresh.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="refreshToken">The new refresh token.</param>
    /// <param name="accessTokenCreationTime">The time when the access token was created.</param>
    private async Task UpdateRefreshTokenAsync(Guid userId, string refreshToken, DateTime accessTokenCreationTime)
    {
        var refreshTokenExpirationAddition = Convert.ToDouble(_configuration["Jwt:RefreshTokenExpiration"]);
        var refreshTokenExpirationTime = accessTokenCreationTime.AddMinutes(refreshTokenExpirationAddition);
        var user = await _userManager.FindByIdAsync(userId.ToString())
            ?? throw new NotFoundException(nameof(AppUser), userId);

        user!.RefreshToken = refreshToken;
        user.RefreshTokenExpiration = refreshTokenExpirationTime;

        await _userManager.UpdateAsync(user);
    }

    #endregion Refresh Token
}