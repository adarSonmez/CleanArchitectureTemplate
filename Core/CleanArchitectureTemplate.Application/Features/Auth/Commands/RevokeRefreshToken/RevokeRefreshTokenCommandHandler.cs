using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.RevokeRefreshToken;

/// <summary>
/// Handles the logic for revoking a user's refresh token.
/// </summary>
public class RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommandRequest, ResponseResult>
{
    private readonly IAuthenticationService _authenticationService;

    public RevokeRefreshTokenCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<ResponseResult> Handle(RevokeRefreshTokenCommandRequest request, CancellationToken cancellationToken)
    {
        await _authenticationService.RevokeRefreshTokenAsync();

        return new();
    }
}