using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Identity;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Users.Commands.RegisterUser;

/// <summary>
/// Represents a request to create a new application user.
/// </summary>
/// <param name="FullName">The full name of the user</param>
/// <param name="Email">The email of the user</param>
/// <param name="UserName">The username of the user</param>
/// <param name="Password">The password of the user</param>
/// <param name="PasswordConfirmation">The password confirmation of the user</param>
/// <param name="PhoneNumber">The phone number of the user</param>
public record RegisterUserCommandRequest
(
    string FullName,
    string Email,
    string UserName,
    string Password,
    string PasswordConfirmation,
    string? PhoneNumber

) : IRequest<SingleResponse<UserDto?>>;