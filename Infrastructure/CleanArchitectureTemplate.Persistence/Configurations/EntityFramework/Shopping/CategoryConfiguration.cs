using CleanArchitectureTemplate.Domain.Entities.Shopping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureTemplate.Persistence.Configurations.EntityFramework.Shopping;

/// <summary>
/// EF Core configuration for the <see cref="Category"/> entity.
/// </summary>
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Table Configuration
        builder.ToTable("Categories", "Shopping");

        // Property Configurations
        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(c => c.Description)
               .HasMaxLength(1000);

        // Relationships
        builder.HasOne(c => c.ParentCategory)
               .WithMany()
               .HasForeignKey("ParentCategoryId")
               .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(c => c.Name).HasDatabaseName("IX_Categories_Name");
    }
}