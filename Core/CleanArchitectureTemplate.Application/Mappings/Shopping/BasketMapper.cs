using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Shopping;

namespace CleanArchitectureTemplate.Application.Mappings.Shopping;

/// <summary>
/// Provides extension methods for mapping between <see cref="Basket"/> and <see cref="BasketDto"/>.
/// </summary>
public static class BasketMapper
{
    /// <summary>
    /// Maps a <see cref="Basket"/> entity to a <see cref="BasketDto"/>.
    /// </summary>
    /// <param name="basket">The basket entity to map.</param>
    /// <returns>The mapped <see cref="BasketDto"/>.</returns>
    public static BasketDto ToDto(this Basket basket)
    {
        if (basket == null) return null!;

        return new BasketDto(
            Id: basket.Id,
            CustomerId: basket.CustomerId,
            TotalAmount: basket.TotalAmount,
            Ordered: basket.Ordered,
            BasketItems: basket.BasketItems?.Select(item => item.ToDto()).ToList()
        );
    }
}