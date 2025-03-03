using CleanArchitectureTemplate.Application.Common.Responses;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.RevokeRefreshToken;

/// <summary>
/// Represents a request to revoke a user's refresh token.
/// </summary>
public record RevokeRefreshTokenCommandRequest() : IRequest<ResponseResult>;