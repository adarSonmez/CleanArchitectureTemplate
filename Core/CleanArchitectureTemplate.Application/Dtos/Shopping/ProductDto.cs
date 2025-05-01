using CleanArchitectureTemplate.Application.Dtos.Files;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;
using CleanArchitectureTemplate.Domain.ValueObjects;

namespace CleanArchitectureTemplate.Application.Dtos.Shopping;

/// <summary>
/// Represents data transfer object for <see cref="Product"/>
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="Name">The name of the product.</param>
/// <param name="Description">The description of the product.</param>
/// <param name="Stock">The stock of the product.</param>
/// <param name="DiscountRate">The discount rate of the product.</param>
/// <param name="StandardPrice">The standard price (non-discounted) of the product.</param>
/// <param name="StoreId">The store that the product belongs to.</param>
/// <param name="DiscountedPrice">The discounted price of the product.</param>
/// <param name="ProductImageFiles">The product images of the product.</param>
/// <param name="Categories">The categories of the product.</param>
public record ProductDto
(
    Guid Id = default,
    string Name = default!,
    string? Description = default,
    int Stock = default,
    decimal DiscountRate = default,
    Money StandardPrice = default!,
    Money DiscountedPrice = default!,
    Guid StoreId = default,
    ICollection<ProductImageFileDto>? ProductImageFiles = default,
    ICollection<CategoryDto>? Categories = default
) : IDto;