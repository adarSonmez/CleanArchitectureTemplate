using CleanArchitectureTemplate.Domain.Entities.Ordering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureTemplate.Persistence.Configurations.EntityFramework.Ordering;

/// <summary>
/// EF Core configuration for the <see cref="OrderItem"/> entity.
/// </summary>
public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        // Table Configuration
        builder.ToTable("OrderItems", "Ordering");

        // Property Configurations
        builder.Property(o => o.Quantity)
               .IsRequired()
               .HasDefaultValue(0);

        builder.Ignore(o => o.TotalPrice);
    }
}