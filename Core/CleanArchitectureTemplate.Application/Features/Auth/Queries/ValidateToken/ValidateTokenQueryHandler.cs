using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Exceptions;
using MediatR;
using System.Security.Claims;

namespace CleanArchitectureTemplate.Application.Features.Auth.Queries.ValidateToken;

/// <summary>
/// Handles the validation of a JWT token.
/// </summary>
public class ValidateTokenQueryHandler : IRequestHandler<ValidateTokenQueryRequest, SingleResponse<ClaimsPrincipal?>>
{
    private readonly ITokenService _tokenService;

    public ValidateTokenQueryHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<SingleResponse<ClaimsPrincipal?>> Handle(ValidateTokenQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<ClaimsPrincipal?>();
        ClaimsPrincipal? principal = null;

        try
        {
            principal = await Task.Run(() => _tokenService.ValidateToken(request.Token), cancellationToken);
        }
        catch
        {
            // Do nothing
        }

        if (principal == null)
        {
            throw new ValidationFailedException("Invalid token.");
        }
        else
        {
            response.SetData(principal);
        }

        return response;
    }
}