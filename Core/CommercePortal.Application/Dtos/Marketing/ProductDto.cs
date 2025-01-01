using CommercePortal.Application.Dtos.Files;
using CommercePortal.Application.Dtos.Ordering;
using CommercePortal.Domain.MarkerInterfaces;
using CommercePortal.Domain.ValueObjects;

namespace CommercePortal.Application.Dtos.Marketing;

/// <summary>
/// Represents the product data transfer object.
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="Name">The name of the product.</param>
/// <param name="Description">The description of the product.</param>
/// <param name="Stock">The stock of the product.</param>
/// <param name="DiscountRate">The discount rate of the product.</param>
/// <param name="StandardPrice">The standard price (non-discounted) of the product.</param>
/// <param name="DiscountedPrice">The discounted price of the product.</param>
/// <param name="ProductImageFile">The product image file data transfer object.</param>
/// <param name="Orders">The orders which include the product.</param>
/// <param name="Categories">The categories of the product.</param>
public record ProductDto
(
    Guid Id,
    string Name,
    string? Description,
    int Stock,
    decimal DiscountRate,
    Money StandardPrice,
    Money DiscountedPrice,
    ProductImageFileDto? ProductImageFile,
    ICollection<OrderDto>? Orders,
    ICollection<CategoryDto>? Categories
) : IDto;