using CleanArchitectureTemplate.Domain.Entities.Shopping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureTemplate.Persistence.Configurations.EntityFramework.Shopping;

/// <summary>
/// EF Core configuration for the <see cref="BasketItem"/> entity.
/// </summary>
public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        // Table Configuration
        builder.ToTable("Categories", "Shopping");
    }
}