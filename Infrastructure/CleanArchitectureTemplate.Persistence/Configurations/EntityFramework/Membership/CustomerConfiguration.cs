using CleanArchitectureTemplate.Domain.Entities.Membership;
using CleanArchitectureTemplate.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureTemplate.Persistence.Configurations.EntityFramework.Membership;

/// <summary>
/// EF Core configuration for the <see cref="Customer"/> entity.
/// </summary>
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        // Table Configuration
        builder.ToTable("Customers", "Membership");

        // Property
        builder.Ignore(c => c.DomainUser);

        // Value Converters
        builder.Property(c => c.Gender)
               .HasConversion<string>()
               .HasMaxLength(15);

        // Relationships
        builder.HasOne<AppUser>()
               .WithOne()
               .HasForeignKey<Customer>(c => c.Id)
               .IsRequired();
    }
}