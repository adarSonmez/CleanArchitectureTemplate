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

        var includes = new List<string>
        {
            nameof(Order.Basket)
        };

        var customerId = _userContextService.GetUserId();

        var (data, totalCount) = await _orderReadRepository.GetAllPaginatedAsync(
            predicate: p => p.Basket!.CustomerId == customerId,
            pagination: request.Pagination,
            includePaths: includes
        );

        if (!request.IncludeBasket)
        {
            foreach (var order in data)
            {
                order.Basket = null;
            }
        }

        response.SetData(
            data.Select(o => o.ToDto()),
            totalCount,
            request.Pagination?.Page,
            request.Pagination?.Size
        );

        return response;
    }
}