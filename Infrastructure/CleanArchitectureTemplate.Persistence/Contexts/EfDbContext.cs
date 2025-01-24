using CleanArchitectureTemplate.Domain.Common;
using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Membership;
using CleanArchitectureTemplate.Domain.Entities.Ordering;
using CleanArchitectureTemplate.Persistence.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CleanArchitectureTemplate.Persistence.Contexts;

/// <summary>
/// Represents the entity framework implementation of the <see cref="IdentityDbContext"/>.
/// </summary>
/// <remarks>
/// Using the <see cref="IdentityDbContext{TUser, TRole, TKey}"/> allows us to use
/// the built-in tables and classes for identity management.
/// </remarks>
public class EfDbContext(DbContextOptions<EfDbContext> options) : IdentityDbContext<AppUser, AppRole, Guid>(options)
{
    #region DbSet Properties

    /// <summary>
    /// Gets or sets the categories table.
    /// </summary>
    public DbSet<Category> Categories { get; set; } = default!;

    /// <summary>
    /// Gets or sets the products table.
    /// </summary>
    public DbSet<Product> Products { get; set; } = default!;

    /// <summary>
    /// Gets or sets the customers table.
    /// </summary>
    public DbSet<Customer> Customers { get; set; } = default!;

    /// <summary>
    /// Gets or sets the stores table.
    /// </summary>
    public DbSet<Store> Stores { get; set; } = default!;

    /// <summary>
    /// Gets or sets the invoices table.
    /// </summary>
    public DbSet<Invoice> Invoices { get; set; } = default!;

    /// <summary>
    /// Gets or sets the orders table.
    /// </summary>
    public DbSet<Order> Orders { get; set; } = default!;

    /// <summary>
    /// Gets or sets the order items table.
    /// </summary>
    public DbSet<OrderItem> OrderItems { get; set; } = default!;

    /// <summary>
    /// Gets or sets the file details table.
    /// </summary>
    public DbSet<FileDetails> FileDetails { get; set; } = default!;

    /// <summary>
    /// Gets or sets the category image files table.
    /// </summary>
    public DbSet<CategoryImageFile> CategoryImageFiles { get; set; } = default!;

    /// <summary>
    /// Gets or sets the invoice files table.
    /// </summary>
    public DbSet<InvoiceFile> InvoiceFiles { get; set; } = default!;

    /// <summary>
    /// Gets or sets the product images table.
    /// </summary>
    public DbSet<ProductImageFile> ProductImageFiles { get; set; } = default!;

    /// <summary>
    /// Gets or sets the report files table.
    /// </summary>
    public DbSet<ReportFile> ReportFiles { get; set; } = default!;

    /// <summary>
    /// Gets or sets the user avatar files table.
    /// </summary>
    public DbSet<UserAvatarFile> UserAvatarFiles { get; set; } = default!;

    #endregion DbSet Properties

    #region Interceptors

    /// <inheritdoc/>
    public override async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        OnBeforeSaveChanges();
        var result = await base.SaveChangesAsync(token);
        OnAfterSaveChanges();

        return result;
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Automatically apply all configurations in the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    /// Performs actions before saving changes in the DbContext.
    /// </summary>
    protected void OnBeforeSaveChanges()
    {
        var entries = ChangeTracker.Entries()
        .Where(e => e is { Entity: BaseEntity, State: EntityState.Added or EntityState.Modified });

        foreach (var entityEntry in entries)
        {
            // TODO: Add shadow properties
            //if (entityEntry.State == EntityState.Added)
            //    entityEntry.Property(CommonShadowProperties.CreatedBy).CurrentValue = DateTime.UtcNow;

            //if (!typeof(FileDetails).IsAssignableFrom(entityEntry.Entity.GetType()))
            //    entityEntry.Property(CommonShadowProperties.UpdatedBy).CurrentValue = DateTime.UtcNow;

            //// Soft delete handling
            //if (entityEntry.Property("IsDeleted").CurrentValue is not bool isDeleted || !isDeleted)
            //    continue;

            //entityEntry.Property(CommonShadowProperties.DeletedBy).CurrentValue = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Performs actions after saving changes in the DbContext.
    /// </summary>
    protected void OnAfterSaveChanges()
    {
    }

    #endregion Interceptors
}