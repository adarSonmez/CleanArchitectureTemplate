using CleanArchitectureTemplate.Domain.Common;
using CleanArchitectureTemplate.Domain.Entities.Membership;
using CleanArchitectureTemplate.Domain.ValueObjects;

namespace CleanArchitectureTemplate.Domain.Entities.Shopping;

/// <summary>
/// Represents a basket entity.
/// </summary>
public class Basket : BaseEntity
{
    /// <summary>
    /// Gets or sets the foreign key for the customer associated with the basket.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the customer who placed the basket.
    /// </summary>
    public Customer Customer { get; set; } = default!;

    /// <summary>
    /// Indicates whether the basket has already been converted into an order.
    /// </summary>
    public bool Ordered { get; set; }

    /// <summary>
    /// The total amount for the order.
    /// </summary>
    public Money? TotalAmount => CalculateTotalAmount();

    /// <summary>
    /// Gets or sets the items in the basket.
    /// </summary>
    public ICollection<BasketItem> BasketItems { get; set; } = [];

    #region Private Methods

    /// <summary>
    /// Computes the total amount of the order.
    /// </summary>
    private Money? CalculateTotalAmount()
    {
        if (BasketItems == null || BasketItems.Count == 0)
        {
            return null;
        }

        var total = BasketItems.Sum(item => item.TotalPrice.Amount);
        return new Money(total, BasketItems.First().TotalPrice.Currency);
    }

    #endregion Private Methods
}