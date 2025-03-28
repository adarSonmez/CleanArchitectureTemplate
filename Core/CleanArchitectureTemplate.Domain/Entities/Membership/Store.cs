﻿using CleanArchitectureTemplate.Domain.Entities.Identity;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Domain.Shared;

namespace CleanArchitectureTemplate.Domain.Entities.Membership;

/// <summary>
/// Represents a store entity.
/// </summary>
public class Store : BaseEntity
{
    /// <summary>
    /// Gets or sets the first name of the customer.
    /// </summary>
    public DomainUser? DomainUser { get; set; }

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