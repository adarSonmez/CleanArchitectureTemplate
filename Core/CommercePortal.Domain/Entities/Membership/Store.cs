using CommercePortal.Domain.Common;
using CommercePortal.Domain.Entities.Identity;
using CommercePortal.Domain.Entities.Marketing;

namespace CommercePortal.Domain.Entities.Membership;

/// <summary>
/// Represents a store entity.
/// </summary>
public class Store : BaseEntity
{
    /// <summary>
    /// Gets or sets the first name of the customer.
    /// </summary>
    public DomainUser DomainUser { get; set; } = default!;

    /// <summary>
    /// Gets or sets foreign key for the user.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the products of the store.
    public ICollection<Product> Products { get; set; } = [];

    /// <summary>
    /// Gets or sets the address of the store.
    /// </summary>
    public string? Website { get; set; }

    /// <summary>
    /// Gets or sets the address of the store.
    /// </summary>
    public string? Description { get; set; }
}