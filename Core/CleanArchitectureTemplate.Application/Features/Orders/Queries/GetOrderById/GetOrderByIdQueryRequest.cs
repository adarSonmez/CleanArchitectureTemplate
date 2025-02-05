using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Ordering;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Orders.Queries.GetOrderById;

/// <summary>
/// Represents the request for getting an Order by ID.
/// </summary>
/// <param name="Id">The ID of the order to get.</param>
/// <param name="IncludeBasket">A flag to include basket.</param>
public record GetOrderByIdQueryRequest
(
    Guid Id,
    bool IncludeBasket
) : IRequest<SingleResponse<OrderDto?>>;