using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.DTOs;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.InternalLogin;

/// <summary>
/// Represents a handler for the <see cref="InternalLoginCommandRequest"/>
/// </summary>
public class InternalLoginCommandHandler : IRequestHandler<InternalLoginCommandRequest, SingleResponse<bool>>
{
    private readonly IAuthenticationService _authenticationService;

    public InternalLoginCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<SingleResponse<bool>> Handle(InternalLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<bool>();
        var tokenDto = await _authenticationService.InternalLoginAsync(request);
        var loggedIn = tokenDto is not null || tokenDto?.AccessToken is not null;

        response.SetData(loggedIn);
        return response;
    }
}