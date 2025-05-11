using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureTemplate.Persistence.Configurations.EntityFramework.Files;

/// <summary>
/// EF Core configuration for the <see cref="UserAvatarFile"/> entity.
/// </summary>
public class UserAvatarFileConfiguration : IEntityTypeConfiguration<UserAvatarFile>
{
    public void Configure(EntityTypeBuilder<UserAvatarFile> builder)
    {
        // Table Configuration
        builder.ToTable("UserAvatarFiles", "Files");

        // Property Configurations
        builder.Ignore(c => c.DomainUser);

        // Relationships
        builder.HasOne<AppUser>()
               .WithOne()
               .HasForeignKey<UserAvatarFile>(c => c.UserId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.HasOne(f => f.FileDetails)
               .WithOne()
               .HasForeignKey<UserAvatarFile>(f => f.FileDetailsId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}