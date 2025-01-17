using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.InternalLogin;
using CleanArchitectureTemplate.Domain.Common;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.InternalLogin;

/// <summary>
/// Represents a handler for the <see cref="InternalLoginCommandRequest"/>
/// </summary>
public class InternalLoginCommandHandler : IRequestHandler<InternalLoginCommandRequest, SingleResponse<TokenDTO?>>
{
    private readonly IAuthenticationService _authenticationService;

    public InternalLoginCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<SingleResponse<TokenDTO?>> Handle(InternalLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<TokenDTO?>();

        try
        {
            var tokenDto = await _authenticationService.InternalLoginAsync(request);
            BusinessRules.Run(("AUR131494", BusinessRules.CheckDtoNull(tokenDto)));

            response.SetData(tokenDto);
        }
        catch (Exception ex)
        {
            response.AddError("AUR618468", ex.Message);
        }

        return response;
    }
}