using CommercePortal.Domain.Entities.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommercePortal.Persistence.Configurations.EntityFramework.Files;

/// <summary>
/// EF Core configuration for the <see cref="ProductImageFile"/> entity.
/// </summary>
public class ProductImageFileConfiguration : IEntityTypeConfiguration<ProductImageFile>
{
    public void Configure(EntityTypeBuilder<ProductImageFile> builder)
    {
        // Table Configuration
        builder.ToTable("ProductImageFiles", "Files");
    }
}