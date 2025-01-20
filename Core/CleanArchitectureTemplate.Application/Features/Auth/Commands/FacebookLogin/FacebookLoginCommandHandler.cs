using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Domain.Exceptions;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.FacebookLogin;

/// <summary>
/// Represents a handler for the <see cref="FacebookLoginAppUserCommandRequest"/>.
/// Handles the logic of verifying a Facebook access token, validating and/or creating a corresponding user,
/// and returning a token for the logged-in user.
/// </summary>
public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, SingleResponse<TokenDto?>>
{
    private readonly IAuthenticationService _authenticationService;

    public FacebookLoginCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<SingleResponse<TokenDto?>> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<TokenDto?>();

        var tokenDto = await _authenticationService.FacebookLoginAsync(request)
            ?? throw new UnauthorizedException("Facebook login failed. Invalid credentials or authentication error.");

        response.SetData(tokenDto);
        return response;
    }
}