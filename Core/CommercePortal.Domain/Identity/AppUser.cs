using Microsoft.AspNetCore.Identity;

namespace CommercePortal.Domain.Identity;

/// <summary>
/// Represents a user in the identity system
/// </summary>
public class AppUser : IdentityUser
{
    /// <summary>
    /// Gets or sets the full name of the identity user
    /// </summary>
    public required string FullName { get; set; }
}