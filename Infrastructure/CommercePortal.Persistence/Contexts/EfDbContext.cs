using CommercePortal.Domain.Entities;
using CommercePortal.Domain.Entities.Common;
using CommercePortal.Domain.Identity;
using CommercePortal.Persistence.Constants;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CommercePortal.Persistence.Contexts;

/// <summary>
/// Represents the entity framework implementation of the <see cref="IdentityDbContext"/>.
/// </summary>
/// <remarks>
/// Using the <see cref="IdentityDbContext{TUser, TRole, TKey}"/> allows us to use
/// the built-in tables and classes for identity management.
/// </remarks>
public class EfDbContext(DbContextOptions<EfDbContext> options) : IdentityDbContext<AppUser, AppRole, string>(options)
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

    /// <summary>
    /// Gets or sets the files table.
    /// </summary>
    public DbSet<Domain.Entities.File> Files { get; set; } = default!;

    /// <summary>
    /// Gets or sets the invoice files table.
    /// </summary>
    public DbSet<InvoiceFile> InvoiceFiles { get; set; } = default!;

    /// <summary>
    /// Gets or sets the product images table.
    /// </summary>
    public DbSet<ProductImageFile> ProductImages { get; set; } = default!;

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

        # region Shadow Properties

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                continue;

            modelBuilder.Entity(entityType.ClrType).Property<DateTime>(CommonShadowProperties.CreatedDate);
            modelBuilder.Entity(entityType.ClrType).Property<DateTime?>(CommonShadowProperties.DeletedDate);

            if (!typeof(Domain.Entities.File).IsAssignableFrom(entityType.ClrType))
                modelBuilder.Entity(entityType.ClrType).Property<DateTime?>(CommonShadowProperties.ModifiedDate);
        }

        # endregion Shadow Properties
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
            if (entityEntry.State == EntityState.Added)
                entityEntry.Property(CommonShadowProperties.CreatedDate).CurrentValue = DateTime.UtcNow;

            if (!typeof(Domain.Entities.File).IsAssignableFrom(entityEntry.Entity.GetType()))
                entityEntry.Property(CommonShadowProperties.ModifiedDate).CurrentValue = DateTime.UtcNow;

            // Soft delete handling
            if (entityEntry.Property("IsDeleted").CurrentValue is not bool isDeleted || !isDeleted)
                continue;

            entityEntry.Property(CommonShadowProperties.DeletedDate).CurrentValue = DateTime.UtcNow;
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