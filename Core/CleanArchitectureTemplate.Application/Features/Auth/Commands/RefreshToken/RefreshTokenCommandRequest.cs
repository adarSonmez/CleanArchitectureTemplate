using CleanArchitectureTemplate.Application.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.RefreshToken;

/// <summary>
/// Represents the command request for refreshing a token.
/// </summary>
/// <param name="RefreshToken">The refresh token to be used to refresh the login of the user</param>
public record RefreshTokenCommandRequest
(
    [FromQuery] string RefreshToken
) : IRequest<ResponseResult>;