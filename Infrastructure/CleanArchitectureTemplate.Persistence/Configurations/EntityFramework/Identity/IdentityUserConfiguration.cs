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
               .HasMaxLength(191);

        builder.Property(u => u.UserName)
            .HasMaxLength(191);

        builder.Property(u => u.NormalizedUserName)
            .HasMaxLength(191);

        builder.Property(u => u.Email)
            .HasMaxLength(191);

        builder.Property(u => u.NormalizedEmail)
            .HasMaxLength(191);

        builder.Property(u => u.FullName)
               .HasMaxLength(255);

        // Indexes
        builder.HasIndex(u => u.RefreshToken)
               .IsUnique();
    }
}