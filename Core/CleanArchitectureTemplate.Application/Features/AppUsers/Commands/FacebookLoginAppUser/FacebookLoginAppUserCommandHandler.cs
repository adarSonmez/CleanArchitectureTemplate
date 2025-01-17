using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Domain.Common;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.AppUsers.Commands.FacebookLoginAppUser;

/// <summary>
/// Represents a handler for the <see cref="FacebookLoginAppUserCommandRequest"/>.
/// Handles the logic of verifying a Facebook access token, validating and/or creating a corresponding user,
/// and returning a token for the logged-in user.
/// </summary>
public class FacebookLoginAppUserCommandHandler : IRequestHandler<FacebookLoginAppUserCommandRequest, SingleResponse<TokenDTO?>>
{
    private readonly IAuthenticationService _authenticationService;

    public FacebookLoginAppUserCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<SingleResponse<TokenDTO?>> Handle(FacebookLoginAppUserCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<TokenDTO?>();

        try
        {
            var tokenDto = await _authenticationService.FacebookLoginAsync(request);
            BusinessRules.Run(("USR957299", BusinessRules.CheckDtoNull(tokenDto)));

            response.SetData(tokenDto);
        }
        catch (Exception ex)
        {
            response.AddError("USR701021", ex.Message);
        }

        return response;
    }
}