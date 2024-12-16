using Microsoft.AspNetCore.Identity;

namespace CommercePortal.Domain.Entities.Identity;

/// <summary>
/// Represents a role in the identity system
/// </summary>
public class AppRole : IdentityRole<Guid>
{
}