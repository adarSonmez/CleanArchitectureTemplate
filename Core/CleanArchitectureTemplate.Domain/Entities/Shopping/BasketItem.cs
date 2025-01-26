using CleanArchitectureTemplate.Domain.Common;
using CleanArchitectureTemplate.Domain.ValueObjects;

namespace CleanArchitectureTemplate.Domain.Entities.Shopping;

/// <summary>
/// Represents a basket entity.
/// </summary>
public class BasketItem : BaseEntity
{
    /// <summary>
    /// Gets or sets the foreign key for the basket that the item belongs to.
    /// </summary>
    public Guid BasketId { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the basket that the item belongs to.
    /// </summary>
    public Basket Basket { get; set; } = default!;

    /// <summary>
    /// Gets or sets the foreign key for the product associated with the item.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the product associated with the item.
    /// </summary>
    public Product Product { get; set; } = default!;

    /// <summary>
    /// Gets or sets the quantity of the product in the basket.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets the total price for this item (price * quantity).
    /// </summary>
    public Money TotalPrice => new(Product.DiscountedPrice.Amount * Quantity, Product.DiscountedPrice.Currency);
}