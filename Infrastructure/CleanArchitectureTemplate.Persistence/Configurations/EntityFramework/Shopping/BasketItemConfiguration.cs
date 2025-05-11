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
        builder.ToTable("BasketItems", "Shopping");

        // Property Configurations
        builder.Ignore(o => o.TotalPrice);

        // Relationships
        builder.HasOne(bi => bi.Basket)
               .WithMany(b => b.BasketItems)
               .HasForeignKey(bi => bi.BasketId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(bi => bi.Product)
                .WithMany()
                .HasForeignKey(bi => bi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}