using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.DTOs;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.RefreshToken;

/// <summary>
/// Represents the command request for refreshing a token.
/// </summary>
/// <param name="UserId">The ID of the user whose login is to be refreshed</param>
/// <param name="RefreshToken">The refresh token to be used to refresh the login</param>
public record RefreshTokenCommandRequest
(
    Guid UserId,
    string RefreshToken
) : IRequest<SingleResponse<TokenDTO?>>;