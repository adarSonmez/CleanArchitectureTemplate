using CleanArchitectureTemplate.Domain.MarkerInterfaces;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureTemplate.Persistence.Identity;

/// <summary>
/// Represents an application user, integrated with ASP.NET Core Identity.
/// </summary>
public class AppUser : IdentityUser<Guid>, IEntity
{
    /// <summary>
    /// Gets or sets the Id of the user.
    /// </summary>
    public override Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Gets or sets refresh token for the user.
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the refresh token expires.
    /// </summary>
    public DateTimeOffset? RefreshTokenExpiration { get; set; }
}