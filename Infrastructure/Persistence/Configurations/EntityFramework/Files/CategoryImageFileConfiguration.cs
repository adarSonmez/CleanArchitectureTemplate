using CleanArchitectureTemplate.Domain.Entities.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureTemplate.Persistence.Configurations.EntityFramework.Files;

/// <summary>
/// EF Core configuration for the <see cref="CategoryImageFile"/> entity.
/// </summary>
public class CategoryImageFileConfiguration : IEntityTypeConfiguration<CategoryImageFile>
{
    public void Configure(EntityTypeBuilder<CategoryImageFile> builder)
    {
        // Table Configuration
        builder.ToTable("CategoryImageFiles", "Files");
    }
}