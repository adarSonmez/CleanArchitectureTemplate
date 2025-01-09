using CleanArchitectureTemplate.Domain.Entities.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureTemplate.Persistence.Configurations.EntityFramework.Files;

/// <summary>
/// EF Core configuration for the <see cref="ReportFile"/> entity.
/// </summary>
public class ReportFileConfiguration : IEntityTypeConfiguration<ReportFile>
{
    public void Configure(EntityTypeBuilder<ReportFile> builder)
    {
        // Table Configuration
        builder.ToTable("ReportFiles", "Files");

        // Value Converters
        builder.Property(fd => fd.ReportType)
            .HasColumnName("ReportType")
            .HasConversion<string>();
    }
}