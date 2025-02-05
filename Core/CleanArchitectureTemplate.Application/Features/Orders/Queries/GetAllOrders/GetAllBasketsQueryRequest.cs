using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Ordering;
using CleanArchitectureTemplate.Application.RequestParameters;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Orders.Queries.GetAllOrders;

/// <summary>
/// Represents the request for getting all Orders.
/// </summary>
/// <param name="Pagination">The pagination parameters.</param
/// <param name="IncludeBasket">A flag to include basket.</param>
public record GetAllOrdersQueryRequest
(
    Pagination? Pagination,
    bool IncludeBasket
) : IRequest<PagedResponse<OrderDto>>;