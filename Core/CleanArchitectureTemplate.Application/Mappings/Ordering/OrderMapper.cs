using CleanArchitectureTemplate.Application.Dtos.Ordering;
using CleanArchitectureTemplate.Application.Features.Orders.Commands.CreateOrderFromBasket;
using CleanArchitectureTemplate.Domain.Entities.Ordering;
using CleanArchitectureTemplate.Domain.ValueObjects;

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
    /// <param name="totalAmount">The calculated total amount of the order.</param>
    /// <param name="customerId">The ID of the customer who placed the order.</param>
    /// <param name="orderItems">The collection of order items for the order.</param>
    /// <returns>The mapped <see cref="OrderDto"/>.</returns>
    public static OrderDto ToDto(this Order order, Money? totalAmount = null, Guid customerId = default, ICollection<OrderItemDto>? orderItems = null)
    {
        if (order == null) return null!;

        return new OrderDto(
            Id: order.Id,
            Status: order.Status,
            TotalAmount: totalAmount,
            ShippingAddress: order.ShippingAddress!,
            CustomerId: customerId,
            BasketId: order.BasketId,
            OrderItems: orderItems
        );
    }
}