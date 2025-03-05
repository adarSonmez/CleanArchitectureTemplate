using CleanArchitectureTemplate.Domain.Entities.Ordering;
using CleanArchitectureTemplate.Persistence.ValueConverters.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureTemplate.Persistence.Configurations.EntityFramework.Ordering;

/// <summary>
/// EF Core configuration for the <see cref="Order"/> entity.
/// </summary>
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        // Table Configuration
        builder.ToTable("Orders", "Ordering");

        // Value Converters
        builder.Property(o => o.Status)
            .HasConversion<string>()
            .HasMaxLength(15);

        // Relationships
        builder.OwnsOne(o => o.ShippingAddress, address =>
        {
            // Making owned type as a seperate table is optional.
            address.ToTable("ShippingAddresses", "Ordering");

            address.WithOwner()
                   .HasForeignKey("OrderId");

            address.Property(a => a.PostalCode)
                   .IsRequired()
                   .HasMaxLength(15);

            address.Property(a => a.City)
                   .IsRequired()
                   .HasMaxLength(255);

            address.Property(a => a.Country)
                   .IsRequired()
                   .HasConversion(new CountryConverter())
                   .HasMaxLength(255);
        });
    }
}