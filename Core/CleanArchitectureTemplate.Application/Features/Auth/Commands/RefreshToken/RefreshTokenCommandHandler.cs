using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.RefreshToken;

/// <summary>
/// Handles the <see cref="RefreshTokenCommandRequest"/>.
/// </summary>
public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommandRequest, ResponseResult>
{
    private readonly IAuthenticationService _authenticationService;

    public RefreshTokenCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<ResponseResult> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
    {
        await _authenticationService.RefreshTokenAsync();

        return new();
    }
}