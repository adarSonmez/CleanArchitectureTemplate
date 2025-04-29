using CleanArchitectureTemplate.Application.Dtos.Ordering;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Application.Features.Products.Commands.CreateProduct;
using CleanArchitectureTemplate.Application.Mappings.Files;
using CleanArchitectureTemplate.Domain.Entities.Shopping;

namespace CleanArchitectureTemplate.Application.Mappings.Shopping;

/// <summary>
/// Provides extension methods for mapping between <see cref="Product"/> and <see cref="ProductDto"/>,
/// as well as from <see cref="CreateProductCommandRequest"/> to <see cref="Product"/>.
/// </summary>
public static class ProductMapper
{
    /// <summary>
    /// Maps a <see cref="CreateProductCommandRequest"/> to a <see cref="Product"/> entity.
    /// </summary>
    /// <param name="request">The product creation request.</param>
    /// <returns>The mapped <see cref="Product"/> entity.</returns>
    public static Product ToEntity(this CreateProductCommandRequest request)
    {
        if (request == null) return null!;

        return new Product
        {
            Name = request.Name,
            Description = request.Description,
            Stock = request.Stock ?? 0,
            DiscountRate = request.DiscountRate ?? 0,
            StandardPrice = request.StandardPrice,
        };
    }

    /// <summary>
    /// Maps a <see cref="Product"/> entity to a <see cref="ProductDto"/>.
    /// </summary>
    /// <param name="product">The <see cref="Product"/> entity to map.</param>
    /// <param name="orderItems">Optional: The orders in which the product is included.</param>
    /// <returns>The mapped <see cref="ProductDto"/>.</returns>
    public static ProductDto ToDto(this Product product, ICollection<OrderDto>? orderItems = null, bool includeCategories = false)
    {
        if (product == null) return null!;

        return new ProductDto(
            Id: product.Id,
            Name: product.Name,
            Description: product.Description,
            Stock: product.Stock,
            DiscountRate: product.DiscountRate,
            StandardPrice: product.StandardPrice,
            DiscountedPrice: product.DiscountedPrice,
            StoreId: product.StoreId,
            ProductImageFiles: product.ProductImageFiles?.Select(p => p.ToDto()).ToList(),
            OrderItems: orderItems,
            Categories: includeCategories ? product.Categories?.Select(c => c.ToDto(includeProducts: false)).ToList() : null
        );
    }
}