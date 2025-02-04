using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.BasketItems.Commands.AddBasketItemToBasket;

/// <summary>
/// Represents the request for adding an item to a basket.
/// </summary>
/// <param name="BasketId">The identifier of the basket.</param>
/// <param name="ProductId">The identifier of the product to add to the basket.</param>
/// <param name="Quantity">The quantity of the product to add to the basket.</param>
public record AddBasketItemToBasketCommandRequest
(
    Guid BasketId = default,
    Guid ProductId = default,
    int Quantity = default
) : IRequest<SingleResponse<BasketDto?>>;