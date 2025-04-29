using CleanArchitectureTemplate.Application.Dtos.Ordering;
using CleanArchitectureTemplate.Application.Features.Orders.Commands.CreateOrderFromBasket;
using CleanArchitectureTemplate.Application.Mappings.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Ordering;

namespace CleanArchitectureTemplate.Application.Mappings.Ordering;

/// <summary>
/// Provides extension methods for mapping between <see cref="Order"/>, <see cref="OrderDto"/>,
/// and <see cref="CreateOrderFromBasketCommandRequest"/>.
/// </summary>
public static class OrderMapper
{
    /// <summary>
    /// Maps a <see cref="CreateOrderFromBasketCommandRequest"/> to an <see cref="Order"/> entity.
    /// </summary>
    /// <param name="request">The request to create an order.</param>
    /// <returns>The mapped <see cref="Order"/> entity.</returns>
    public static Order ToEntity(this CreateOrderFromBasketCommandRequest request)
    {
        if (request == null) return null!;

        return new Order
        {
            BasketId = request.BasketId,
            ShippingAddress = request.ShippingAddress
        };
    }

    /// <summary>
    /// Maps an <see cref="Order"/> entity to an <see cref="OrderDto"/>.
    /// </summary>
    /// <param name="order">The order entity to map.</param>
    /// <returns>The mapped <see cref="OrderDto"/>.</returns>
    public static OrderDto ToDto(this Order order)
    {
        if (order == null) return null!;

        return new OrderDto(
            Id: order.Id,
            Status: order.Status,
            Basket: order.Basket?.ToDto()
        );
    }
}