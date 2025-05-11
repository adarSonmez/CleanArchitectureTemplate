using CleanArchitectureTemplate.Domain.Entities.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CleanArchitectureTemplate.Persistence.Configurations.EntityFramework.Files;

/// <summary>
/// EF Core configuration for the <see cref="ProductImageFile"/> entity.
/// </summary>
public class ProductImageFileConfiguration : IEntityTypeConfiguration<ProductImageFile>
{
    public void Configure(EntityTypeBuilder<ProductImageFile> builder)
    {
        // Table Configuration
        builder.ToTable("ProductImageFiles", "Files");

        // Relationships
        builder.HasOne(f => f.Product)
               .WithMany(f => f.ProductImageFiles)
               .HasForeignKey(f => f.ProductId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(f => f.FileDetails)
               .WithOne()
               .HasForeignKey<ProductImageFile>(f => f.FileDetailsId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}