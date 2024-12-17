using Microsoft.AspNetCore.Identity;

namespace CommercePortal.Domain.Entities.Identity;

/// <summary>
/// Represents a user in the identity system
/// </summary>
public class AppUser : IdentityUser<Guid>
{
    /// <summary>
    /// Gets the identifier of the user
    /// </summary>
    public override Guid Id => Guid.NewGuid();

    /// <summary>
    /// Gets or sets the full name of the identity user
    /// </summary>
    public required string FullName { get; set; }
}