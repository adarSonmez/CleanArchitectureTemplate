using CleanArchitectureTemplate.Domain.Entities.Ordering;
using CleanArchitectureTemplate.Persistence.ValueConverters.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureTemplate.Persistence.Configurations.EntityFramework.Ordering;

/// <summary>
/// EF Core configuration for the <see cref="Invoice"/> entity.
/// </summary>
public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        // Table Configuration
        builder.ToTable("Invoices", "Ordering");

        // Property Configurations
        builder.Property(i => i.TransactionId)
               .HasMaxLength(50);

        builder.Property(i => i.Notes)
               .HasMaxLength(1000);

        // Relationships
        builder.OwnsOne(o => o.BillingAddress, address =>
        {
            // Making owned type as a seperate table is optional.
            address.ToTable("BillingAddress", "Ordering");

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

        // Value Converters
        builder.Property(i => i.Status)
            .HasConversion<string>()
            .HasMaxLength(15);

        builder.Property(i => i.PaymentMethod)
            .HasConversion<string>()
            .HasMaxLength(15);
    }
}