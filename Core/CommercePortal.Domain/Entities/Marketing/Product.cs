using CommercePortal.Domain.Common;
using CommercePortal.Domain.Entities.Files;
using CommercePortal.Domain.Entities.Ordering;
using CommercePortal.Domain.ValueObjects;

namespace CommercePortal.Domain.Entities.Marketing;

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
    /// Gets or sets the standard price (non-discounted) of the product.
    /// </summary>
    public Money StandardPrice { get; set; } = default!;

    /// <summary>
    /// Gets or sets the discount rate of the product.
    /// </summary>
    public decimal DiscountRate { get; set; }

    /// <summary>
    /// Gets the discounted price of the product.
    /// </summary>
    public Money DiscountedPrice => new(StandardPrice.Amount * (1 - DiscountRate), StandardPrice.Currency);

    /// <summary>
    /// Gets or sets the orders which include the product.
    /// </summary>
    public ICollection<Order> Orders { get; set; } = [];

    /// <summary>
    /// Gets or sets the product image files.
    /// </summary>
    public ICollection<ProductImageFile> ProductImageFiles { get; set; } = [];

    /// <summary>
    /// Gets or sets the categories of the product.
    /// </summary>
    public IList<Category> Categories { get; set; } = [];
}