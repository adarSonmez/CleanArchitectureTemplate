using CleanArchitectureTemplate.Domain.MarkerInterfaces;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureTemplate.Persistence.Identity;

/// <summary>
/// Represents an application role, integrated with ASP.NET Core Identity.
/// </summary>
public class AppRole : IdentityRole<Guid>, IEntity
{
}