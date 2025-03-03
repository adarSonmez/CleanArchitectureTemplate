using CleanArchitectureTemplate.Application.Common.Responses;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.Logout;

/// <summary>
/// Represents a request to log out a user.
/// </summary>
/// <param name="UserId">The ID of the user to log out.</param>
public record LogoutCommandRequest() : IRequest<ResponseResult>;