using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Features.AppUsers.Commands.LoginAppUser;
using CleanArchitectureTemplate.Application.Features.Commands.AppUsers.FacebookLoginAppUser;
using CleanArchitectureTemplate.Application.Features.Commands.AppUsers.GoogleLoginAppUser;
using CleanArchitectureTemplate.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureTemplate.Persistence.Services.Auth.Identity;

public class IdentityAuthenticationService : IAuthenticationService
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<AppUser> _userManager;
    public readonly SignInManager<AppUser> _signInManager;

    public IdentityAuthenticationService(ITokenService tokenService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    /// <inheritdoc />
    public async Task<TokenDTO?> InternalLoginAsync(LoginAppUserCommandRequest model)
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
        {
            throw new Exception("User not found");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

        if (result.Succeeded)
        {
            return _tokenService.GenerateToken(user.Id, false);
        }
        else
        {
            throw new Exception("Invalid password");
        }
    }

    /// <inheritdoc />
    public async Task<TokenDTO?> FacebookLoginAsync(FacebookLoginAppUserCommandRequest model)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<TokenDTO?> GoogleLoginAsync(GoogleLoginAppUserCommandRequest model)
    {
        throw new System.NotImplementedException();
    }
}