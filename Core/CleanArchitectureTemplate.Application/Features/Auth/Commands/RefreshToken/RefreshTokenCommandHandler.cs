using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.DTOs;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.RefreshToken;

/// <summary>
/// Handles the <see cref="RefreshTokenCommandRequest"/>.
/// </summary>
public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommandRequest, SingleResponse<bool>>
{
    private readonly IAuthenticationService _authenticationService;

    public RefreshTokenCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<SingleResponse<bool>> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<bool>();
        var tokenDto = await _authenticationService.RefreshTokenAsync(request);
        var loggedIn = tokenDto is not null || tokenDto?.AccessToken is not null;

        response.SetData(loggedIn);

        return response;
    }
}