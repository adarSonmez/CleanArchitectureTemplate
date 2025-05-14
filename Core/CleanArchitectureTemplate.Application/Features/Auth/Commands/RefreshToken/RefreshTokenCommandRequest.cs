using CleanArchitectureTemplate.Application.Common.Responses;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.RefreshToken;

/// <summary>
/// Represents the command request for refreshing a token.
/// </summary>
public record RefreshTokenCommandRequest() : IRequest<ResponseResult>;