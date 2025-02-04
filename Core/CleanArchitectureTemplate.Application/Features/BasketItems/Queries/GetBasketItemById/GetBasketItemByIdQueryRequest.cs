using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.BasketItems.Queries.GetBasketItemById;

/// <summary>
/// Represents the request for getting a basket item by its ID.
/// </summary>
/// <param name="Id">The basket item ID.</param>
public record GetBasketItemByIdQueryRequest
(
    Guid Id
) : IRequest<SingleResponse<BasketItemDto?>>;