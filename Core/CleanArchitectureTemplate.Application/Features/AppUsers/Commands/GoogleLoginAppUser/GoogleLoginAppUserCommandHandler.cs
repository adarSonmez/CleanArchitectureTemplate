using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Domain.Common;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.AppUsers.Commands.GoogleLoginAppUser;

/// <summary>
/// Represents a handler for the <see cref="GoogleLoginAppUserCommandRequest"/>
/// </summary>
public class GoogleLoginAppUserCommandHandler : IRequestHandler<GoogleLoginAppUserCommandRequest, SingleResponse<TokenDTO?>>
{
    private readonly IAuthenticationService _authenticationService;

    public GoogleLoginAppUserCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<SingleResponse<TokenDTO?>> Handle(GoogleLoginAppUserCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<TokenDTO?>();

        try
        {
            var tokenDto = await _authenticationService.GoogleLoginAsync(request);
            BusinessRules.Run(("USR697198", BusinessRules.CheckDtoNull(tokenDto)));

            response.SetData(tokenDto);
        }
        catch (Exception ex)
        {
            response.AddError("USR376285", ex.Message);
        }

        return response;
    }
}