using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Persistence.ValueConverters.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CleanArchitectureTemplate.Persistence.Configurations.EntityFramework.Shopping;

/// <summary>
/// Configuration for the Product entity.
/// </summary>
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Table Configuration
        builder.ToTable("Products", "Shopping");

        builder.HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity<Dictionary<string, object>>(
                "ProductCategory",
                j => j.HasOne<Category>().WithMany().HasForeignKey("CategoryId"),
                j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId"),
                j =>
                {
                    j.HasKey("ProductId", "CategoryId");
                    j.ToTable("ProductCategories", "Shopping");
                });

        // Property Configurations
        builder.Ignore(p => p.DiscountedPrice);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(191);

        builder.Property(p => p.DiscountRate)
               .HasPrecision(5, 2)
               .IsRequired();

        // Relationships
        builder.HasOne(p => p.Store)
               .WithMany(s => s.Products)
               .HasForeignKey(p => p.StoreId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.StandardPrice)
            .HasConversion(new MoneyConverter())
            .HasMaxLength(50);

        // Indexes
        builder.HasIndex(p => p.Name).HasDatabaseName("IX_Products_Name");
    }
}