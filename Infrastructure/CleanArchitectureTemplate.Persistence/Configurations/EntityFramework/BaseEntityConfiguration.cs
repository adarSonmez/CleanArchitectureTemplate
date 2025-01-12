using CleanArchitectureTemplate.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureTemplate.Persistence.Configurations.EntityFramework;

/// <summary>
/// Configuration for the BaseEntity class.
/// </summary>
public class BaseEntityConfiguration : IEntityTypeConfiguration<BaseEntity>
{
    public void Configure(EntityTypeBuilder<BaseEntity> builder)
    {
        builder.HasKey(e => e.Id);
    }
}