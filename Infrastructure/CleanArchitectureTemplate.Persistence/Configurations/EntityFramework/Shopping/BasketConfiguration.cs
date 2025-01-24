using CleanArchitectureTemplate.Domain.Entities.Shopping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureTemplate.Persistence.Configurations.EntityFramework.Shopping;

/// <summary>
/// EF Core configuration for the <see cref="Basket"/> entity.
/// </summary>
public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        // Table Configuration
        builder.ToTable("Categories", "Shopping");
    }
}