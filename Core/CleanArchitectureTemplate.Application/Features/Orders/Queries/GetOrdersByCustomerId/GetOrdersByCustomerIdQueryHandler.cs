using CleanArchitectureTemplate.Application.Abstractions.Repositories.Ordering;
using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Ordering;
using CleanArchitectureTemplate.Application.Features.Orders.Queries.GetOrderById;
using CleanArchitectureTemplate.Application.Mappings.Ordering;
using CleanArchitectureTemplate.Domain.Entities.Ordering;
using MediatR;
using System.Linq.Expressions;

namespace CleanArchitectureTemplate.Application.Features.Orders.Queries.GetOrdersByCustomerId;

/// <summary>
/// Handles the <see cref="GetOrderByIdQueryRequest"/>.
/// </summary>
public class GetOrdersByCustomerIdQueryHandler : IRequestHandler<GetOrdersByCustomerIdQueryRequest, PagedResponse<OrderDto>>
{
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IUserContextService _userContextService;

    public GetOrdersByCustomerIdQueryHandler(IOrderReadRepository OrderReadRepository, IUserContextService userContextService)
    {
        _orderReadRepository = OrderReadRepository;
        _userContextService = userContextService;
    }

    public async Task<PagedResponse<OrderDto>> Handle(GetOrdersByCustomerIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new PagedResponse<OrderDto>();

        var includes = new List<Expression<Func<Order, object>>>
        {
            p => p.Basket!
        };

        var customerId = _userContextService.GetUserId();

        var orders = await _orderReadRepository.GetAllPaginatedAsync(
            predicate: p => p.Basket!.CustomerId == customerId,
            pagination: request.Pagination,
            include: includes
        );

        if (!request.IncludeBasket)
        {
            foreach (var order in orders)
            {
                order.Basket = null;
            }
        }

        response.SetData(
            orders.Select(o => o.ToDto()),
            request.Pagination?.Page,
            request.Pagination?.Size
        );

        return response;
    }
}