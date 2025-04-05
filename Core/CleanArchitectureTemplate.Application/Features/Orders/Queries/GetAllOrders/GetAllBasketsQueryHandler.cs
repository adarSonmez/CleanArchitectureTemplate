using CleanArchitectureTemplate.Application.Abstractions.Repositories.Ordering;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Ordering;
using CleanArchitectureTemplate.Application.Mappings.Ordering;
using CleanArchitectureTemplate.Domain.Entities.Ordering;
using MediatR;
using System.Linq.Expressions;

namespace CleanArchitectureTemplate.Application.Features.Orders.Queries.GetAllOrders;

/// <summary>
/// Handles the <see cref="GetAllOrdersQueryRequest"/>.
/// </summary>
public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, PagedResponse<OrderDto>>
{
    private readonly IOrderReadRepository _orderReadRepository;

    public GetAllOrdersQueryHandler(IOrderReadRepository OrderReadRepository)
    {
        _orderReadRepository = OrderReadRepository;
    }

    public async Task<PagedResponse<OrderDto>> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new PagedResponse<OrderDto>();

        var includes = new List<Expression<Func<Order, object>>>();
        if (request.IncludeBasket)
        {
            includes.Add(p => p.Basket!);
        }

        var orders = await _orderReadRepository.GetAllPaginatedAsync(
            pagination: request.Pagination,
            include: includes
        );

        response.SetData(
            orders.Select(o => o.ToDto()),
            request.Pagination?.Page,
            request.Pagination?.Size
        );

        return response;
    }
}