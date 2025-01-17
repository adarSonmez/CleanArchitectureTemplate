using CleanArchitectureTemplate.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureTemplate.Persistence.Configurations.EntityFramework.Identity;

/// <summary>
/// EF Core configuration for the <see cref="AppUser"/> entity.
/// </summary>
public class IdentityUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        // Table Configuration
        builder.ToTable("Users", "Identity");

        // Property Configurations
        builder.Property(u => u.RefreshToken)
               .HasMaxLength(255)
               .IsRequired(false);

        // Indexes
        builder.HasIndex(u => u.RefreshToken)
               .IsUnique();
    }
}