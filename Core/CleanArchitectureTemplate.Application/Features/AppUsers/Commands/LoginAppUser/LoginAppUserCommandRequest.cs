using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.DTOs;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.AppUsers.Commands.LoginAppUser;

/// <summary>
/// Represents a request to log in a user
/// </summary>
/// <param name="Email">The email of the user</param>
/// <param name="UserName">The username of the user</param>
/// <param name="Password">The password of the user</param>
public record LoginAppUserCommandRequest
(
    string? Email,
    string? UserName,
    string Password
) : IRequest<SingleResponse<TokenDTO?>>;