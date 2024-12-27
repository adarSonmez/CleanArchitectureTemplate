namespace CommercePortal.Application.Dtos.Identity;

using CommercePortal.Domain.MarkerInterfaces;

/// <summary>
/// Represents the user data transfer object.
/// </summary>
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