using CleanArchitectureTemplate.Application.Common.Responses;
using MediatR;
using System.Security.Claims;

namespace CleanArchitectureTemplate.Application.Features.Auth.Queries.ValidateToken;

/// <summary>
/// Represents a request to validate a JWT token.
/// </summary>
/// <param name="Token">The JWT token to be validated.</param>
public record ValidateTokenQueryRequest(string Token) : IRequest<SingleResponse<ClaimsPrincipal?>>;