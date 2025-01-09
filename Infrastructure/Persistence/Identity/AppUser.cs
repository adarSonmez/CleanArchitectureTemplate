using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureTemplate.Persistence.Identity;

/// <summary>
/// Represents an application user, integrated with ASP.NET Core Identity.
/// </summary>
public class AppUser : IdentityUser<Guid>
{
    /// <summary>
    /// Gets or sets the Id of the user.
    /// </summary>
    public override Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    public required string FullName { get; set; }
}