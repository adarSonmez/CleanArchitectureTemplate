using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Domain.Common;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.GoogleLogin;

/// <summary>
/// Represents a handler for the <see cref="GoogleLoginCommandRequest"/>
/// </summary>
public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, SingleResponse<TokenDto?>>
{
    private readonly IAuthenticationService _authenticationService;

    public GoogleLoginCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<SingleResponse<TokenDto?>> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<TokenDto?>();

        try
        {
            var tokenDto = await _authenticationService.GoogleLoginAsync(request);
            BusinessRules.Run(("AUR697198", BusinessRules.CheckDtoNull(tokenDto)));

            response.SetData(tokenDto);
        }
        catch (Exception ex)
        {
            response.AddError("AUR376285", ex.Message);
        }

        return response;
    }
}