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
    /// The total amount for the order.
    /// </summary>
    public Money? TotalAmount => CalculateTotalAmount();

    /// <summary>
    /// Gets or sets the address of the order.
    /// </summary>
    public Address ShippingAddress { get; set; } = default!;

    /// <summary>
    /// Gets or sets the foreign key for the invoice associated with the order.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the customer who placed the order.
    /// </summary>
    public Customer Customer { get; set; } = default!;

    /// <summary>
    /// Gets or sets the order items associated with the order.
    /// </summary>
    public ICollection<OrderItem> OrderItems { get; set; } = [];

    #region Private Methods

    /// <summary>
    /// Computes the total amount of the order.
    /// </summary>
    private Money? CalculateTotalAmount()
    {
        if (OrderItems == null || OrderItems.Count == 0)
        {
            return null;
        }

        var total = OrderItems.Sum(item => item.TotalPrice.Amount);
        return new Money(total, OrderItems.First().TotalPrice.Currency);
    }

    #endregion Private Methods
}