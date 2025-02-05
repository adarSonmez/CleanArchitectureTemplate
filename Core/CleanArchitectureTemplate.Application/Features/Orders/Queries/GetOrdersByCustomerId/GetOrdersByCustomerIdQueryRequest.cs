using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Ordering;
using CleanArchitectureTemplate.Application.RequestParameters;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Orders.Queries.GetOrdersByCustomerId;

/// <summary>
/// Represents the request for getting Orders by Customer ID.
/// </summary>
/// <param name="CustomerId">The ID of the customer to get orders for.</param>
/// <param name="Pagination">The pagination parameters.</param
/// <param name="IncludeBasket">A flag to include basket.</param>
public record GetOrdersByCustomerIdQueryRequest
(
    Guid CustomerId,
    Pagination? Pagination,
    bool IncludeBasket
) : IRequest<PagedResponse<OrderDto>>;