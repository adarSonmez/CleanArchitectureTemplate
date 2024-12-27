using CommercePortal.Application.Dtos.Files;
using CommercePortal.Application.Dtos.Ordering;
using CommercePortal.Domain.MarkerInterfaces;
using CommercePortal.Domain.ValueObjects;

namespace CommercePortal.Application.Dtos.Marketing;

/// <summary>
/// Represents the product data transfer object.
/// </summary>
public record ProductDto
(
    Guid Id,
    string Name,
    string? Description,
    int Stock,
    decimal DiscountRate,
    Money StandardPrice,
    Money DiscountedPrice,
    ICollection<OrderDto>? Orders,
    ICollection<ProductImageFileDto>? ProductImageFiles,
    ICollection<CategoryDto>? Categories
) : IDto;