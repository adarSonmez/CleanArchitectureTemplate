using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Application.Features.BasketItems.Commands.AddBasketItemToBasket;
using CleanArchitectureTemplate.Domain.Entities.Shopping;

namespace CleanArchitectureTemplate.Application.Mappings.Shopping;

/// <summary>
/// Provides extension methods for mapping between <see cref="BasketItem"/>,
/// <see cref="BasketItemDto"/>, and <see cref="AddBasketItemToBasketCommandRequest"/>.
/// </summary>
public static class BasketItemMapper
{
    /// <summary>
    /// Maps an <see cref="AddBasketItemToBasketCommandRequest"/> to a <see cref="BasketItem"/> entity.
    /// </summary>
    /// <param name="request">The request to add an item to a basket.</param>
    /// <returns>The mapped <see cref="BasketItem"/> entity.</returns>
    public static BasketItem ToEntity(this AddBasketItemToBasketCommandRequest request)
    {
        if (request == null) return null!;

        return new BasketItem
        {
            BasketId = request.BasketId,
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };
    }

    /// <summary>
    /// Maps a <see cref="BasketItem"/> entity to a <see cref="BasketItemDto"/>.
    /// </summary>
    /// <param name="entity">The <see cref="BasketItem"/> entity to map.</param>
    /// <returns>The mapped <see cref="BasketItemDto"/>.</returns>
    public static BasketItemDto ToDto(this BasketItem entity)
    {
        if (entity == null) return null!;

        return new BasketItemDto(
            Id: entity.Id,
            BasketId: entity.BasketId,
            ProductId: entity.ProductId,
            Quantity: entity.Quantity
        );
    }
}