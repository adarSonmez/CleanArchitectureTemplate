using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.Logout;

/// <summary>
/// Handles the logic for logging out a user by revoking their refresh token and signing them out.
/// </summary>
public class LogoutCommandHandler : IRequestHandler<LogoutCommandRequest, ResponseResult>
{
    private readonly IAuthenticationService _authenticationService;

    public LogoutCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<ResponseResult> Handle(LogoutCommandRequest request, CancellationToken cancellationToken)
    {
        await _authenticationService.LogoutAsync();

        return new();
    }
}