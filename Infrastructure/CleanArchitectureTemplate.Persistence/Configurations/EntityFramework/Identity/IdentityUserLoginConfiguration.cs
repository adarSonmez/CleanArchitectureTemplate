using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureTemplate.Persistence.Configurations.EntityFramework.Identity;

/// <summary>
/// EF Core configuration for the <see cref="IdentityUserLogin{TKey}"/> entity.
/// </summary>
public class IdentityUserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<Guid>> builder)
    {
        // Table Configuration
        builder.ToTable("UserLogins", "Identity");

        // Property Configurations
        builder.Property(ul => ul.LoginProvider)
            .HasMaxLength(191);

        builder.Property(ul => ul.ProviderDisplayName)
            .HasMaxLength(255);
    }
}