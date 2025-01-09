using CleanArchitectureTemplate.Domain.Common;
using CleanArchitectureTemplate.Domain.Entities.Marketing;
using CleanArchitectureTemplate.Domain.ValueObjects;

namespace CleanArchitectureTemplate.Domain.Entities.Ordering;

/// <summary>
/// Represents an order item entity.
/// </summary>
public class OrderItem : BaseEntity
{
    /// <summary>
    /// Gets or sets foreign key for the product.
    /// </summary>
    public Guid OrderId { get; set; }

    /// <summary>
    /// Gets or sets the order this item belongs to.
    /// </summary>
    public Order Order { get; set; } = default!;

    /// <summary>
    /// Gets or sets the foreign key for the product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the product for this item.
    /// </summary>
    public Product Product { get; set; } = default!;

    /// <summary>
    /// Gets or sets the quantity of the product in this order.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets the total price for this item (price * quantity).
    /// </summary>
    public Money TotalPrice => new(Product.DiscountedPrice.Amount * Quantity, Product.DiscountedPrice.Currency);
}