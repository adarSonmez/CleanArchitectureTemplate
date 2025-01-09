using CleanArchitectureTemplate.Domain.Common;
using CleanArchitectureTemplate.Domain.Constants.Enums;
using CleanArchitectureTemplate.Domain.Entities.Identity;
using CleanArchitectureTemplate.Domain.Entities.Ordering;

namespace CleanArchitectureTemplate.Domain.Entities.Membership;

/// <summary>
/// Represents a customer entity.
/// </summary>
public class Customer : BaseEntity
{
    /// <summary>
    /// Gets or sets the domain user associated with the customer.
    /// </summary>
    public DomainUser DomainUser { get; set; } = default!;

    /// <summary>
    /// Gets or sets foreign key for the user.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the orders of the customer.
    /// </summary>
    public ICollection<Order> Orders { get; set; } = [];

    /// <summary>
    /// Gets or sets the age of the customer.
    /// </summary>
    public required short Age { get; set; }

    /// <summary>
    /// Gets or sets the gender of the customer.
    /// </summary>
    public Gender Gender { get; set; } = Gender.Unspecified;
}