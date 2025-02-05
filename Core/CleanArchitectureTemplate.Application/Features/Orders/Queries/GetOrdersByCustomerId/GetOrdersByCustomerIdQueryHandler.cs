using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Ordering;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Ordering;
using CleanArchitectureTemplate.Application.Features.Orders.Queries.GetOrderById;
using CleanArchitectureTemplate.Domain.Entities.Ordering;
using MediatR;
using System.Linq.Expressions;

namespace CleanArchitectureTemplate.Application.Features.Orders.Queries.GetOrdersByCustomerId;

/// <summary>
/// Handles the <see cref="GetOrderByIdQueryRequest"/>.
/// </summary>
public class GetOrdersByCustomerIdQueryHandler : IRequestHandler<GetOrdersByCustomerIdQueryRequest, PagedResponse<OrderDto>>
{
    private readonly IMapper _mapper;
    private readonly IOrderReadRepository _orderReadRepository;

    public GetOrdersByCustomerIdQueryHandler(IMapper mapper, IOrderReadRepository OrderReadRepository)
    {
        _mapper = mapper;
        _orderReadRepository = OrderReadRepository;
    }

    public async Task<PagedResponse<OrderDto>> Handle(GetOrdersByCustomerIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new PagedResponse<OrderDto>();

        var includes = new List<Expression<Func<Order, object>>>();
        if (request.IncludeBasket)
        {
            includes.Add(p => p.Basket);
        }

        var orders = await _orderReadRepository.GetAllPaginatedAsync(
            predicate: p => p.Basket.CustomerId == request.CustomerId,
            pagination: request.Pagination,
            include: includes
        );

        response.SetData(
            _mapper.Map<IEnumerable<OrderDto>>(orders),
            request.Pagination?.Page,
            request.Pagination?.Size
        );

        return response;
    }
}