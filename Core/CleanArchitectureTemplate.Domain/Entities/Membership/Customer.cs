using CleanArchitectureTemplate.Domain.Constants.Enums;
using CleanArchitectureTemplate.Domain.Entities.Identity;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Domain.Shared;

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
    /// Gets or sets the baskets of the customer. (include both ordered and not ordered baskets)
    /// </summary>
    public ICollection<Basket> Baskets { get; set; } = [];

    /// <summary>
    /// Gets or sets the date of birth of the customer.
    /// </summary>
    public required DateOnly DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the gender of the customer.
    /// </summary>
    public Gender Gender { get; set; } = Gender.Unspecified;
}