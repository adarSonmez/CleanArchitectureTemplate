using CommercePortal.Domain.Entities.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommercePortal.Persistence.Configurations.EntityFramework.Files;

/// <summary>
/// EF Core configuration for the <see cref="UserAvatarFile"/> entity.
/// </summary>
public class UserAvatarFileConfiguration : IEntityTypeConfiguration<UserAvatarFile>
{
    public void Configure(EntityTypeBuilder<UserAvatarFile> builder)
    {
        // Table Configuration
        builder.ToTable("UserAvatarFiles", "Files");
    }
}