using CleanArchitectureTemplate.Domain.Common;
using CleanArchitectureTemplate.Domain.Entities.Membership;

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
    /// Gets or sets the items in the basket.
    /// </summary>
    public ICollection<BasketItem> BasketItems { get; set; } = [];
}