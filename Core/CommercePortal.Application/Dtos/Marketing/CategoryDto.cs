namespace CommercePortal.Application.Dtos.Marketing;

using CommercePortal.Application.Dtos.Files;
using CommercePortal.Domain.MarkerInterfaces;

/// <summary>
/// Represents the category data transfer object.
/// </summary>
public record CategoryDto
(
    Guid Id,
    string Name,
    string? Description,
    Guid? ParentCategoryId,
    CategoryImageFileDto? CategoryImageFile,
    IEnumerable<ProductDto>? Products
) : IDto;