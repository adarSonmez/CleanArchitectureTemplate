using CleanArchitectureTemplate.Domain.Common;
using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Domain.Entities.Membership;
using CleanArchitectureTemplate.Domain.Entities.Ordering;
using CleanArchitectureTemplate.Domain.ValueObjects;

namespace CleanArchitectureTemplate.Domain.Entities.Shopping;

/// <summary>
/// Represents a product entity.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the stock of the product.
    /// </summary>
    public int Stock { get; set; }

    /// <summary>
    /// Gets or sets the discount rate of the product.
    /// </summary>
    public decimal DiscountRate { get; set; }

    /// <summary>
    /// Gets or sets the standard price (non-discounted) of the product.
    /// </summary>
    public required Money StandardPrice { get; set; }

    /// <summary>
    /// Gets the discounted price of the product.
    /// </summary>
    public Money DiscountedPrice => new(StandardPrice.Amount * (1 - DiscountRate), StandardPrice.Currency);

    /// <summary>
    /// Gets or sets the store that the product belongs to.
    /// </summary>
    public Guid StoreId { get; set; }

    /// <summary>
    /// Gets or sets the store that the product belongs to.
    /// </summary>
    public Store Store { get; set; } = default!;

    /// <summary>
    /// Gets or sets the product image files.
    /// </summary>
    public ICollection<ProductImageFile> ProductImageFiles { get; set; } = [];

    /// <summary>
    /// Gets or sets the categories of the product.
    /// </summary>
    public IList<Category> Categories { get; set; } = [];

    /// <summary>
    /// Gets or sets the basket items where the product is included.
    /// </summary>
    public ICollection<BasketItem> BasketItems { get; set; } = [];
}