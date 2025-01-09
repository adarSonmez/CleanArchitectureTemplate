using CleanArchitectureTemplate.Domain.Entities.Identity;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.Identity;

/// <summary>
/// Represents data transfer object for <see cref="DomainUser"/>
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="FullName">The full name of the user.</param>
/// <param name="UserName">The user name.</param>
/// <param name="Email">The email address of the user.</param>
/// <param name="EmailConfirmed">A flag indicating whether the email is confirmed.</param>
/// <param name="PhoneNumber">The phone number of the user.</param>
/// <param name="PhoneNumberConfirmed">A flag indicating whether the phone number is confirmed.</param>
/// <param name="TwoFactorEnabled">A flag indicating whether two factor authentication is enabled.</param>
public record UserDto
(
    Guid? Id,
    string? FullName,
    string UserName,
    string Email,
    bool EmailConfirmed,
    string? PhoneNumber,
    bool PhoneNumberConfirmed,
    bool TwoFactorEnabled
) : IDto;