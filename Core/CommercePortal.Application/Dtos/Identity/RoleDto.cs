namespace CommercePortal.Application.Dtos.Identity;

using CommercePortal.Domain.MarkerInterfaces;

/// <summary>
/// Represents the role data transfer object.
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="Name">The name of the role.</param>
public record RoleDto
(
    Guid Id,
    string Name
) : IDto;