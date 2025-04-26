using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Identity;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Users.Commands.UpdateUser;

/// <summary>
/// Represents a request to update an existing application user.
/// </summary>
/// <param name="Id">The unique identifier of the user</param>
/// <param name="FullName">The full name of the user</param>
/// <param name="Email">The email of the user</param>
/// <param name="UserName">The username of the user</param>
/// <param name="PhoneNumber">The phone number of the user</param>
public record UpdateUserCommandRequest
(
    Guid Id,
    string? FullName,
    string? Email,
    string? UserName,
    string? PhoneNumber
) : IRequest<SingleResponse<UserDto?>>;