using Microsoft.AspNetCore.Identity;

namespace CommercePortal.Persistence.Identity;

/// <summary>
/// Represents an application user, integrated with ASP.NET Core Identity.
/// </summary>
public class AppUser : IdentityUser<Guid>
{
    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    public string? FullName { get; set; }
}