using CleanArchitectureTemplate.Domain.Entities.Membership;
using CleanArchitectureTemplate.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureTemplate.Persistence.Configurations.EntityFramework.Membership;

/// <summary>
/// EF Core configuration for the <see cref="Store"/> entity.
/// </summary>
public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        // Table Configuration
        builder.ToTable("Stores", "Membership");

        // Property Configurations
        builder.Property(s => s.Website)
               .HasMaxLength(200) // Limit URL length to 200 characters
               .IsRequired(false);

        builder.Property(s => s.Description)
               .HasMaxLength(1000) // Limit description to 1000 characters
               .IsRequired(false);

        builder.Ignore(s => s.DomainUser);

        // Relationships
        builder.HasOne<AppUser>()
               .WithOne()
               .HasForeignKey<Store>(s => s.UserId)
               .IsRequired();
    }
}