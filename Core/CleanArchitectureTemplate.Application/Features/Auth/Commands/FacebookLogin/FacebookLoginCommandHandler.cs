using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.FacebookLogin;

/// <summary>
/// Represents a handler for the <see cref="FacebookLoginCommandRequest"/>.
/// Handles the logic of verifying a Facebook access token, validating and/or creating a corresponding user,
/// and returning a token for the logged-in user.
/// </summary>
public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, SingleResponse<bool>>
{
    private readonly IAuthenticationService _authenticationService;

    public FacebookLoginCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<SingleResponse<bool>> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<bool>();

        var tokenDto = await _authenticationService.FacebookLoginAsync(request);
        var loggedIn = tokenDto is not null || tokenDto?.AccessToken is not null;

        response.SetData(loggedIn);
        return response;
    }
}