using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.DTOs;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.GoogleLogin;

/// <summary>
/// Represents a handler for the <see cref="GoogleLoginCommandRequest"/>
/// </summary>
public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, SingleResponse<bool>>
{
    private readonly IAuthenticationService _authenticationService;

    public GoogleLoginCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<SingleResponse<bool>> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<bool>();
        var tokenDto = await _authenticationService.GoogleLoginAsync(request);
        var loggedIn = tokenDto is not null || tokenDto?.AccessToken is not null;

        response.SetData(loggedIn);

        return response;
    }
}