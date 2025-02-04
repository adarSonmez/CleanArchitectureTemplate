using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Baskets.Commads.ClearBasket;

/// <summary>
/// Represents the request for removing an item from a basket.
/// </summary>
/// <param name="BasketId">The identifier of the basket.</param>
public record ClearBasketCommandRequest
(
    Guid BasketId = default
) : IRequest<SingleResponse<BasketDto?>>;