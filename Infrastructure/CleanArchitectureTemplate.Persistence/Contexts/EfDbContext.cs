using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Domain.Entities.Membership;
using CleanArchitectureTemplate.Domain.Entities.Ordering;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Domain.Shared;
using CleanArchitectureTemplate.Persistence.Constants;
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
public class EfDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    private readonly IUserContextService _userContextService;

    public EfDbContext(DbContextOptions<EfDbContext> options, IUserContextService userContextService)
        : base(options)
    {
        _userContextService = userContextService;
    }

    #region DbSet Properties

    #region Shopping DbSet Properties

    /// <summary>
    /// Gets or sets the categories table.
    /// </summary>
    public DbSet<Category> Categories { get; set; } = default!;

    /// <summary>
    /// Gets or sets the products table.
    /// </summary>
    public DbSet<Product> Products { get; set; } = default!;

    /// <summary>
    /// Gets or sets the baskets table.
    /// </summary>
    public DbSet<Basket> Baskets { get; set; } = default!;

    /// <summary>
    /// Gets or sets the basket items table.
    /// </summary>
    public DbSet<BasketItem> BasketItems { get; set; } = default!;

    #endregion Shopping DbSet Properties

    #region Membership DbSet Properties

    /// <summary>
    /// Gets or sets the customers table.
    /// </summary>
    public DbSet<Customer> Customers { get; set; } = default!;

    /// <summary>
    /// Gets or sets the stores table.
    /// </summary>
    public DbSet<Store> Stores { get; set; } = default!;

    #endregion Membership DbSet Properties

    #region Ordering DbSet Properties

    /// <summary>
    /// Gets or sets the invoices table.
    /// </summary>
    public DbSet<Invoice> Invoices { get; set; } = default!;

    /// <summary>
    /// Gets or sets the orders table.
    /// </summary>
    public DbSet<Order> Orders { get; set; } = default!;

    #endregion Ordering DbSet Properties

    #region Files DbSet Properties

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

    #endregion Files DbSet Properties

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

        modelBuilder.Entity<BaseEntity>(builder =>
        {
            builder.Property<string>(CommonShadowProperties.CreatedBy).HasMaxLength(191);
            builder.Property<string>(CommonShadowProperties.UpdatedBy).HasMaxLength(191);
            builder.Property<string>(CommonShadowProperties.DeletedBy).HasMaxLength(191);

            switch (Database.ProviderName)
            {
                case "Microsoft.EntityFrameworkCore.SqlServer":
                    builder.Property(e => e.RowVersion)
                            .IsRowVersion()
                            .HasColumnName("RowVersion");
                    builder.Ignore(e => e.Version);
                    break;

                case "Npgsql.EntityFrameworkCore.PostgreSQL":
                    builder.Property(e => e.Version)
                            .IsRowVersion()
                            .HasColumnName("Version");
                    builder.Ignore(e => e.RowVersion);
                    break;

                default:
                    builder.Ignore(e => e.RowVersion);
                    builder.Ignore(e => e.Version);
                    break;
            }
        });
    }

    /// <summary>
    /// Performs actions before saving changes in the DbContext.
    /// </summary>
    protected void OnBeforeSaveChanges()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            if (entityEntry.Entity is BaseEntity entity)
            {
                var now = DateTime.UtcNow;
                var currentUser = _userContextService.GetUserName();

                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        entityEntry.Property(CommonShadowProperties.CreatedBy).CurrentValue = currentUser;
                        entity.CreatedAt = now;
                        break;

                    case EntityState.Modified:
                        if (entityEntry.Metadata.FindProperty("IsDeleted") != null
                            && entityEntry.Property("IsDeleted")?.CurrentValue is bool isDeleted && isDeleted)
                        {
                            entityEntry.Property(CommonShadowProperties.DeletedBy).CurrentValue = currentUser;
                            entity.DeletedAt = now;
                        }
                        else
                        {
                            entityEntry.Property(CommonShadowProperties.UpdatedBy).CurrentValue = currentUser;
                            entity.UpdatedAt = now;
                        }

                        break;
                }
            }
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