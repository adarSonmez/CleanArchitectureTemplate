using CleanArchitectureTemplate.Domain.Entities.Marketing;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.Marketing;

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
    Guid Id,
    string Name,
    string? Description,
    Guid? ParentCategoryId,
    IEnumerable<ProductDto>? Products
) : IDto;