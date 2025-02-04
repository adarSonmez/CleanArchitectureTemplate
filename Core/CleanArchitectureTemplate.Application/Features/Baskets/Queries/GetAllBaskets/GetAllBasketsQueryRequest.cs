using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Application.RequestParameters;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Baskets.Queries.GetAllBaskets;

/// <summary>
/// Represents the request for getting all baskets.
/// </summary>
/// <param name="Pagination">The pagination parameters.</param
/// <param name="IncludeBasketItems">A flag to include basket items.</param>
public record GetAllBasketsQueryRequest
(
    Pagination? Pagination,
    bool IncludeBasketItems
) : IRequest<PagedResponse<BasketDto>>;