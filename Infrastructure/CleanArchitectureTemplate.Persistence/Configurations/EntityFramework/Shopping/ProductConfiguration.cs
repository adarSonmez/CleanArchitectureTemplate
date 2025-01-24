using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Persistence.ValueConverters.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

        // Property Configurations
        builder.Ignore(p => p.DiscountedPrice);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(p => p.Stock)
               .IsRequired()
               .HasDefaultValue(0);

        builder.Property(p => p.DiscountRate)
               .HasPrecision(5, 2)
               .IsRequired();

        // Relationships
        builder.Property(p => p.StandardPrice)
            .HasConversion(new MoneyConverter());

        // Indexes
        builder.HasIndex(p => p.Name).HasDatabaseName("IX_Products_Name");
    }
}