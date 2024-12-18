using CommercePortal.Application.Abstractions.Token;
using CommercePortal.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CommercePortal.Application.Features.Commands.AppUsers.LoginAppUser;

/// <summary>
/// Represents a handler for the <see cref="LoginAppUserCommandRequest"/>
/// </summary>
public class LoginAppUserCommandHandler : IRequestHandler<LoginAppUserCommandRequest, LoginAppUserCommandResponse>
{
    public readonly UserManager<AppUser> _userManager;
    public readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;

    public LoginAppUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
    }

    public async Task<LoginAppUserCommandResponse> Handle(LoginAppUserCommandRequest request, CancellationToken cancellationToken)
    {
        AppUser? user = null;

        if (!string.IsNullOrEmpty(request.UserName))
        {
            user = await _userManager.FindByNameAsync(request.UserName);
        }
        else if (!string.IsNullOrEmpty(request.Email))
        {
            user = await _userManager.FindByEmailAsync(request.Email);
        }

        if (user == null)
        {
            throw new Exception("User not found");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (result.Succeeded)
        {
            var token = _tokenHandler.GenerateToken(user.Id, false);

            return new LoginAppUserCommandResponse(token);
        }
        else
        {
            throw new Exception("Invalid password");
        }
    }
}