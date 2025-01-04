using CommercePortal.Domain.Entities.Identity;
using CommercePortal.Domain.MarkerInterfaces;

namespace CommercePortal.Application.Dtos.Identity;

/// <summary>
/// Represent data transfer object for <see cref="DomainRole"/>
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="Name">The name of the role.</param>
public record RoleDto
(
    Guid Id,
    string Name
) : IDto;