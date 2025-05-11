using CleanArchitectureTemplate.Domain.Entities.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureTemplate.Persistence.Configurations.EntityFramework.Files;

/// <summary>
/// EF Core configuration for the <see cref="InvoiceFile"/> entity.
/// </summary>
public class InvoiceFileConfiguration : IEntityTypeConfiguration<InvoiceFile>
{
    public void Configure(EntityTypeBuilder<InvoiceFile> builder)
    {
        // Table Configuration
        builder.ToTable("InvoiceFiles", "Files");

        // Relationships
        builder.HasOne(f => f.FileDetails)
               .WithOne()
               .HasForeignKey<InvoiceFile>(f => f.FileDetailsId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}