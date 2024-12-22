using Microsoft.AspNetCore.Identity;

namespace CommercePortal.Persistence.Identity;

/// <summary>
/// Represents an application role, integrated with ASP.NET Core Identity.
/// </summary>
public class AppRole : IdentityRole<Guid>
{
}