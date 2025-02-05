using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Ordering;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Ordering;
using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Domain.Entities.Ordering;
using MediatR;
using System.Linq.Expressions;

namespace CleanArchitectureTemplate.Application.Features.Orders.Queries.GetOrderById;

/// <summary>
/// Handles the <see cref="GetOrderByIdQueryRequest"/>.
/// </summary>
public class GetAllOrdersQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, SingleResponse<OrderDto?>>
{
    private readonly IMapper _mapper;
    private readonly IOrderReadRepository _orderReadRepository;

    public GetAllOrdersQueryHandler(IMapper mapper, IOrderReadRepository OrderReadRepository)
    {
        _mapper = mapper;
        _orderReadRepository = OrderReadRepository;
    }

    public async Task<SingleResponse<OrderDto?>> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<OrderDto?>();

        var includes = new List<Expression<Func<Order, object>>>();
        if (request.IncludeBasket)
        {
            includes.Add(p => p.Basket);
        }

        var order = await _orderReadRepository.GetByIdAsync(request.Id, include: includes)
            ?? throw new NotFoundException(nameof(Order), request.Id);

        response.SetData(_mapper.Map<OrderDto>(order));

        return response;
    }
}