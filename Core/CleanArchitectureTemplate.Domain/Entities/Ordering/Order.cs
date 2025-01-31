using CleanArchitectureTemplate.Domain.Constants.Enums;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Domain.Shared;
using CleanArchitectureTemplate.Domain.ValueObjects;

namespace CleanArchitectureTemplate.Domain.Entities.Ordering;

/// <summary>
/// Represents an order entity.
/// </summary>
public class Order : BaseEntity
{
    /// <summary>
    /// Gets or sets the status of the order.
    /// </summary>
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    /// <summary>
    /// Gets or sets the address of the order.
    /// </summary>
    public Address ShippingAddress { get; set; } = default!;

    /// <summary>
    /// Gets or sets the foreign key for the basket associated with the order.
    /// </summary>
    public Guid BasketId { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the basket associated with the order.
    /// </summary>
    public Basket Basket { get; set; } = default!;
}