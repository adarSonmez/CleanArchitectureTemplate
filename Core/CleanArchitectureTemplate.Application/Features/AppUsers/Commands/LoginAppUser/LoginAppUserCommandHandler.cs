using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Domain.Common;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.AppUsers.Commands.LoginAppUser;

/// <summary>
/// Represents a handler for the <see cref="LoginAppUserCommandRequest"/>
/// </summary>
public class LoginAppUserCommandHandler : IRequestHandler<LoginAppUserCommandRequest, SingleResponse<TokenDTO?>>
{
    private readonly IAuthenticationService _authenticationService;

    public LoginAppUserCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<SingleResponse<TokenDTO?>> Handle(LoginAppUserCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<TokenDTO?>();

        try
        {
            var tokenDto = await _authenticationService.InternalLoginAsync(request);
            BusinessRules.Run(("USR131494", BusinessRules.CheckDtoNull(tokenDto)));

            response.SetData(tokenDto);
        }
        catch (Exception ex)
        {
            response.AddError("USR618468", ex.Message);
        }

        return response;
    }
}