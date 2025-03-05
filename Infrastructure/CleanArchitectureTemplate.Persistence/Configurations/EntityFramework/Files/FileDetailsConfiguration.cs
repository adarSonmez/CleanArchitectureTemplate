using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Persistence.ValueConverters.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureTemplate.Persistence.Configurations.EntityFramework.Files;

/// <summary>
/// Configuration for the FileDetails entity.
/// </summary>
public class FileDetailsConfiguration : IEntityTypeConfiguration<FileDetails>
{
    public void Configure(EntityTypeBuilder<FileDetails> builder)
    {
        // Table Configuration
        builder.ToTable("FileDetails", "Files");

        // Property Configurations
        builder.Property(f => f.Name)
               .IsRequired()
               .HasMaxLength(191);

        builder.Property(f => f.Folder)
               .IsRequired()
               .HasMaxLength(255)
               .HasDefaultValue(string.Empty);

        // Indexes
        builder.HasIndex(f => f.Name).HasDatabaseName("IX_FileDetails_Name");

        // Value Converters
        builder.Property(fd => fd.Extension)
            .HasColumnName("FileType")
            .HasConversion(new FileExtensionConverter())
            .HasMaxLength(32);

        builder.Property(fd => fd.Storage)
            .HasColumnName("StorageType")
            .HasConversion<string>();
    }
}