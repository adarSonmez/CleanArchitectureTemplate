namespace CommercePortal.Application.Dtos.Identity;

using CommercePortal.Domain.MarkerInterfaces;

/// <summary>
/// Represents the role data transfer object.
/// </summary>
public record RoleDto
(
    Guid Id,
    string Name
) : IDto;