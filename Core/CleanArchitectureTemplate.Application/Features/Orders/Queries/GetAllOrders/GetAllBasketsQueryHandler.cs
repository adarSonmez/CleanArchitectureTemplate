using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Ordering;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Ordering;
using CleanArchitectureTemplate.Domain.Entities.Ordering;
using MediatR;
using System.Linq.Expressions;

namespace CleanArchitectureTemplate.Application.Features.Orders.Queries.GetAllOrders;

/// <summary>
/// Handles the <see cref="GetAllOrdersQueryRequest"/>.
/// </summary>
public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, PagedResponse<OrderDto>>
{
    private readonly IMapper _mapper;
    private readonly IOrderReadRepository _orderReadRepository;

    public GetAllOrdersQueryHandler(IMapper mapper, IOrderReadRepository OrderReadRepository)
    {
        _mapper = mapper;
        _orderReadRepository = OrderReadRepository;
    }

    public async Task<PagedResponse<OrderDto>> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new PagedResponse<OrderDto>();

        var includes = new List<Expression<Func<Order, object>>>();
        if (request.IncludeBasket)
        {
            includes.Add(p => p.Basket);
        }

        // Fetch paginated Orders from the repository
        var orders = await _orderReadRepository.GetAllPaginatedAsync(
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