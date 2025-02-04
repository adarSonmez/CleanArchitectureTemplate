using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Baskets.Queries.GetBasketById;

/// <summary>
/// Represents the request for getting a basket by its ID.
/// </summary>
/// <param name="Id">The basket ID.</param>
/// <param name="IncludeBasketItems">A flag to include items.</param>
public record GetBasketByIdQueryRequest
(
    Guid Id,
    bool IncludeBasketItems
) : IRequest<SingleResponse<BasketDto?>>;