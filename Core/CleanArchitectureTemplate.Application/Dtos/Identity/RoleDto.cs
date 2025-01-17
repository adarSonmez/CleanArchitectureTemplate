using CleanArchitectureTemplate.Domain.Entities.Identity;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.Identity;

/// <summary>
/// Represent data transfer object for <see cref="DomainRole"/>
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="Name">The name of the role.</param>
public record RoleDto
(
    Guid Id = default,
    string Name = default!
) : IDto;