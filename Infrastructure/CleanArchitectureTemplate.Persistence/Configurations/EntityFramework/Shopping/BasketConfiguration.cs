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
        builder.ToTable("Baskets", "Shopping");

        // Property Configurations
        builder.Ignore(o => o.TotalAmount);

        // Relationships
        builder.HasOne(b => b.Customer)
               .WithMany(c => c.Baskets)
               .HasForeignKey(b => b.CustomerId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}