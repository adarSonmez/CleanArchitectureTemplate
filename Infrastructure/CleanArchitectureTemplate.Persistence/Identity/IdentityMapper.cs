/**
 * NOTE TO DEVELOPERS:
 *
 * This is the only mapper profile that is located in the Persistence project.
 * This is because it is responsible for mapping between the domain entities and the identity entities,
 * which are specific to the infrastructure layer.
 * Other mapping profiles are located in the Application project to ensure that the domain layer remains isolated from infrastructure concerns.
*/

using CleanArchitectureTemplate.Application.Dtos.Identity;
using CleanArchitectureTemplate.Application.Features.Users.Commands.RegisterUser;
using CleanArchitectureTemplate.Application.Features.Users.Commands.UpdateUser;
using CleanArchitectureTemplate.Domain.Entities.Identity;

namespace CleanArchitectureTemplate.Persistence.Identity;

/// <summary>
/// Provides custom extension methods for mapping identity-related types
/// between domain and infrastructure layers.
/// </summary>
public static class IdentityMapper
{
    /// <summary>
    /// Maps a <see cref="DomainUser"/> to an <see cref="AppUser"/>.
    /// </summary>
    public static AppUser ToAppUser(this DomainUser user)
    {
        if (user == null) return null!;

        return new AppUser
        {
            Id = user.Id,
            FullName = user.FullName ?? string.Empty,
            UserName = user.UserName,
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
            PhoneNumber = user.PhoneNumber,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            TwoFactorEnabled = user.TwoFactorEnabled,
            LockoutEnabled = user.LockoutEnabled,
            LockoutEnd = user.LockoutEnd,
            AccessFailedCount = user.AccessFailedCount,
            RefreshToken = user.RefreshToken,
            RefreshTokenExpiration = user.RefreshTokenExpiration,
            PasswordHash = user.PasswordHash
        };
    }

    /// <summary>
    /// Maps an <see cref="AppUser"/> to a <see cref="DomainUser"/>.
    /// </summary>
    public static DomainUser ToDomainUser(this AppUser user)
    {
        if (user == null) return null!;

        return new DomainUser
        {
            Id = user.Id,
            FullName = string.IsNullOrWhiteSpace(user.FullName) ? null! : user.FullName,
            UserName = user.UserName!,
            Email = user.Email!,
            EmailConfirmed = user.EmailConfirmed,
            PhoneNumber = user.PhoneNumber,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            TwoFactorEnabled = user.TwoFactorEnabled,
            LockoutEnabled = user.LockoutEnabled,
            LockoutEnd = user.LockoutEnd,
            AccessFailedCount = user.AccessFailedCount,
            RefreshToken = user.RefreshToken,
            RefreshTokenExpiration = user.RefreshTokenExpiration,
            PasswordHash = user.PasswordHash!
        };
    }

    /// <summary>
    /// Maps a <see cref="RegisterUserCommandRequest"/> to an <see cref="AppUser"/>.
    /// </summary>
    public static AppUser ToAppUser(this RegisterUserCommandRequest request)
    {
        if (request == null) return null!;

        return new AppUser
        {
            UserName = request.UserName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            FullName = request.FullName
        };
    }

    /// <summary>
    /// Maps an <see cref="AppUser"/> to a <see cref="UserDto"/>.
    /// </summary>
    public static UserDto ToDto(this AppUser user)
    {
        if (user == null) return null!;

        return new UserDto(
            Id: user.Id,
            FullName: user.FullName,
            UserName: user.UserName!,
            Email: user.Email!,
            EmailConfirmed: user.EmailConfirmed,
            PhoneNumber: user.PhoneNumber,
            PhoneNumberConfirmed: user.PhoneNumberConfirmed,
            TwoFactorEnabled: user.TwoFactorEnabled
        );
    }

    /// <summary>
    /// Maps a <see cref="DomainRole"/> to an <see cref="AppRole"/>.
    /// </summary>
    public static AppRole ToAppRole(this DomainRole role)
    {
        if (role == null) return null!;
        return new AppRole { Id = role.Id, Name = role.Name };
    }

    /// <summary>
    /// Maps an <see cref="AppRole"/> to a <see cref="DomainRole"/>.
    /// </summary>
    public static DomainRole ToDomainRole(this AppRole role)
    {
        if (role == null) return null!;
        return new DomainRole { Id = role.Id, Name = role.Name ?? string.Empty };
    }
}