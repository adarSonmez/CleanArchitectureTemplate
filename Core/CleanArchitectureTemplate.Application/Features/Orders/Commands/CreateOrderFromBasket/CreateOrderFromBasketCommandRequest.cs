using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Ordering;
using CleanArchitectureTemplate.Domain.ValueObjects;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Orders.Commands.CreateOrderFromBasket;

/// <summary>
/// Represents the request to create an order from a basket.
/// </summary>
/// <param name="BasketId">The ID of the basket to create the order from.</param>
/// <param name="ShippingAddress">The shipping address for the order.</param>
public record CreateOrderFromBasketCommandRequest
(
    Guid BasketId,
    Address ShippingAddress
) : IRequest<SingleResponse<OrderDto?>>;