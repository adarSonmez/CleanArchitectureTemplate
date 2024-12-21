using CommercePortal.Domain.Common;
using CommercePortal.Domain.Constants.Enums;
using CommercePortal.Domain.Entities.Membership;
using CommercePortal.Domain.ValueObjects;

namespace CommercePortal.Domain.Entities.Ordering;

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
    /// Gets or sets the navigation property to the customer who placed the order.
    /// </summary>
    public Customer Customer { get; set; } = default!;

    /// <summary>
    /// Gets or sets the navigation property to the invoice associated with the order.
    /// </summary>
    public Invoice Invoice { get; set; } = default!;

    /// <summary>
    /// Gets or sets the order items associated with the order.
    /// </summary>
    public ICollection<OrderItem> OrderItems { get; set; } = [];
}