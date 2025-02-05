using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Ordering;
using CleanArchitectureTemplate.Domain.Constants.Enums;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Orders.Commands.UpdateOrderStatus;

/// <summary>
/// Represents the request to create an order from a basket.
/// </summary>
/// <param name="Id">The ID of the order to update.</param>
/// <param name="Status">The new status of the order.</param>
public record UpdateOrderStatusCommandRequest
(
    Guid Id,
    OrderStatus Status
) : IRequest<SingleResponse<OrderDto?>>;