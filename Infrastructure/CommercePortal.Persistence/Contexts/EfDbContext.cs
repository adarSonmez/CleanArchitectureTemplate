using CommercePortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommercePortal.Persistence.Contexts;

/// <summary>
/// Represents the entity framework implementation of the <see cref="DbContext"/>.
/// </summary>
public class EfDbContext(DbContextOptions<EfDbContext> options) : DbContext(options)
{
    #region DbSet Properties

    /// <summary>
    /// Gets or sets the products table.
    /// </summary>
    public DbSet<Product> Products { get; set; } = default!;

    /// <summary>
    /// Gets or sets the customers table.
    /// </summary>
    public DbSet<Customer> Customers { get; set; } = default!;

    /// <summary>
    /// Gets or sets the orders table.
    /// </summary>
    public DbSet<Order> Orders { get; set; } = default!;

    #endregion DbSet Properties
}