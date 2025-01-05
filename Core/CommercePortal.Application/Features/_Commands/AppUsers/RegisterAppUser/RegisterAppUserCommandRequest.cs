using MediatR;

namespace CommercePortal.Application.Features.Commands.AppUsers.RegisterAppUser;

/// <summary>
/// Represents a request to create a new user
/// </summary>
/// <param name="FullName">The full name of the user</param>
/// <param name="Email">The email of the user</param>
/// <param name="UserName">The username of the user</param>
/// <param name="Password">The password of the user</param>
/// <param name="PasswordConfirmation">The password confirmation of the user</param>
/// <param name="PhoneNumber">The phone number of the user</param>
public record RegisterAppUserCommandRequest
(
    string FullName,
    string Email,
    string UserName,
    string Password,
    string PasswordConfirmation,
    string? PhoneNumber

) : IRequest<RegisterAppUserCommandResponse>;