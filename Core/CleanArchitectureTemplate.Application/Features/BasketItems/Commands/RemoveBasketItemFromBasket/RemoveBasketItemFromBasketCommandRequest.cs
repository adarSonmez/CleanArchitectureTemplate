using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.BasketItems.Commands.RemoveBasketItemFromBasket;

/// <summary>
/// Represents the request for removing an item from a basket.
/// </summary>
/// <param name="BasketId">The identifier of the basket.</param>
/// <param name="ProductId">The identifier of the product to remove from the basket.</param>
/// <param name="Quantity">The quantity of the product to remove from the basket.</param>
/// <param name="Clear">If true, removes the entire product from the basket, ignoring Quantity.</param>
public record RemoveBasketItemFromBasketCommandRequest
(
    Guid BasketId = default,
    Guid ProductId = default,
    int Quantity = 1,
    bool Clear = false
) : IRequest<SingleResponse<BasketDto?>>;