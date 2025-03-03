using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.GoogleLogin;

/// <summary>
/// Represents a handler for the <see cref="GoogleLoginCommandRequest"/>
/// </summary>
public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, ResponseResult>
{
    private readonly IAuthenticationService _authenticationService;

    public GoogleLoginCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<ResponseResult> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
    {
        await _authenticationService.GoogleLoginAsync(request);

        return new();
    }
}