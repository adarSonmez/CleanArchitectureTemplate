using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.RevokeRefreshToken;

/// <summary>
/// Handles the logic for revoking a user's refresh token.
/// </summary>
public class RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommandRequest, SingleResponse<bool>>
{
    private readonly IAuthenticationService _authenticationService;

    public RevokeRefreshTokenCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<SingleResponse<bool>> Handle(RevokeRefreshTokenCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<bool>();

        await _authenticationService.RevokeRefreshTokenAsync(request.UserId);

        response.SetData(true);
        return response;
    }
}