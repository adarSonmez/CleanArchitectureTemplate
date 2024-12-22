using CommercePortal.Domain.Entities.Marketing;
using CommercePortal.Persistence.ValueConverters.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommercePortal.Persistence.Configurations.EntityFramework.Marketing;

/// <summary>
/// Configuration for the Product entity.
/// </summary>
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Table Configuration
        builder.ToTable("Products", "Marketing");

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