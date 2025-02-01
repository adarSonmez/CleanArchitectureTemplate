using CleanArchitectureTemplate.Application.Common.Responses;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.RevokeRefreshToken;

/// <summary>
/// Represents a request to revoke a user's refresh token.
/// </summary>
/// <param name="UserId">The ID of the user whose refresh token should be revoked.</param>
public record RevokeRefreshTokenCommandRequest(Guid UserId) : IRequest<SingleResponse<bool>>;