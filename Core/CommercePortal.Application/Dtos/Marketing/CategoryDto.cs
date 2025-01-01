namespace CommercePortal.Application.Dtos.Marketing;

using CommercePortal.Application.Dtos.Files;
using CommercePortal.Domain.MarkerInterfaces;

/// <summary>
/// Represents the category data transfer object.
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="Name">The name of the category.</param>
/// <param name="Description">The description of the category.</param>
/// <param name="ParentCategoryId">The identifier of the parent category.</param>
/// <param name="CategoryImageFile">The category image file data transfer object.</param>
/// <param name="Products">The products that belong to the category.</param>
public record CategoryDto
(
    Guid Id,
    string Name,
    string? Description,
    Guid? ParentCategoryId,
    CategoryImageFileDto? CategoryImageFile,
    IEnumerable<ProductDto>? Products
) : IDto;