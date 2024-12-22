using CommercePortal.Domain.Entities.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommercePortal.Persistence.Configurations.EntityFramework.Files;

/// <summary>
/// EF Core configuration for the <see cref="InvoiceFile"/> entity.
/// </summary>
public class InvoiceFileConfiguration : IEntityTypeConfiguration<InvoiceFile>
{
    public void Configure(EntityTypeBuilder<InvoiceFile> builder)
    {
        // Table Configuration
        builder.ToTable("InvoiceFiles", "Files");
    }
}