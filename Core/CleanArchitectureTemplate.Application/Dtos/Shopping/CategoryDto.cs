using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.Shopping;

/// <summary>
/// Represents data transfer object for <see cref="Category"/>
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="Name">The name of the category.</param>
/// <param name="Description">The description of the category.</param>
/// <param name="ParentCategoryId">The identifier of the parent category.</param>
/// <param name="Products">The products that belong to the category.</param>
public record CategoryDto
(
    Guid Id = default,
    string Name = default!,
    string? Description = default,
    Guid? ParentCategoryId = default,
    IEnumerable<ProductDto>? Products = default
) : IDto;