using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Membership;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.Membership;

/// <summary>
/// Represents data transfer object for <see cref="Store"/>
/// </summary>
/// <param name="UserId">The user id of the store.</param>
/// <param name="Website">The website of the store.</param>
/// <param name="Description">The description of the store.</param>
/// <param name="Products">The products that the store has.</param>
public record StoreDto
(
    Guid UserId = default!,
    string? Website = null,
    string? Description = null,
    ICollection<ProductDto>? Products = null
) : IDto;