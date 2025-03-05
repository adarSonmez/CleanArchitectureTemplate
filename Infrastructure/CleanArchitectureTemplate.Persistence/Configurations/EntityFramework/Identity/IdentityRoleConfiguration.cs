using CleanArchitectureTemplate.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureTemplate.Persistence.Configurations.EntityFramework.Identity;

/// <summary>
/// EF Core configuration for the <see cref="AppRole"/> entity.
/// </summary>
public class IdentityRoleConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        // Table Configuration
        builder.ToTable("Roles", "Identity");

        // Property Configurations
        builder.Property(r => r.Name)
            .HasMaxLength(191);

        builder.Property(r => r.NormalizedName)
            .HasMaxLength(191);
    }
}