using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.InternalLogin;

/// <summary>
/// Represents a handler for the <see cref="InternalLoginCommandRequest"/>
/// </summary>
public class InternalLoginCommandHandler : IRequestHandler<InternalLoginCommandRequest, ResponseResult>
{
    private readonly IAuthenticationService _authenticationService;

    public InternalLoginCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<ResponseResult> Handle(InternalLoginCommandRequest request, CancellationToken cancellationToken)
    {
        await _authenticationService.InternalLoginAsync(request);

        return new();
    }
}