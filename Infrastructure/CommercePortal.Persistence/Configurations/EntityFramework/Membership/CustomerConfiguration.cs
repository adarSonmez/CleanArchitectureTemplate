using CommercePortal.Domain.Entities.Membership;
using CommercePortal.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommercePortal.Persistence.Configurations.EntityFramework.Membership;

/// <summary>
/// EF Core configuration for the <see cref="Customer"/> entity.
/// </summary>
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        // Table Configuration
        builder.ToTable("Customers", "Membership");

        // Property Configurations
        builder.Property(c => c.Age)
            .HasDefaultValue(null)
            .HasColumnType("smallint");

        builder.Ignore(c => c.DomainUser);

        // Value Converters
        builder.Property(c => c.Gender)
               .HasConversion<string>();

        // Relationships
        builder.HasOne<AppUser>()
               .WithOne()
               .HasForeignKey<Customer>(c => c.UserId)
               .IsRequired();
    }
}