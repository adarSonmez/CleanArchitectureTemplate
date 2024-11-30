using CommercePortal.Domain.Entities.Common;

namespace CommercePortal.Domain.Entities;

/// <summary>
/// Represents an order entity.
/// </summary>
public class Order : BaseEntity
{
    /// <summary>
    /// Gets or sets the address of the order.
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the customer who placed the order.
    /// </summary>
    public Customer Customer { get; set; } = default!;

    /// <summary>
    /// Gets or sets the products which are included in the order.
    /// </summary>
    public ICollection<Product> Products { get; set; } = [];
}