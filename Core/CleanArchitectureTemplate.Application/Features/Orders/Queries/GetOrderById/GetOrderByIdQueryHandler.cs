using CleanArchitectureTemplate.Application.Abstractions.Repositories.Ordering;
using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Ordering;
using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Application.Mappings.Ordering;
using CleanArchitectureTemplate.Domain.Entities.Ordering;
using MediatR;
using System.Linq.Expressions;

namespace CleanArchitectureTemplate.Application.Features.Orders.Queries.GetOrderById;

/// <summary>
/// Handles the <see cref="GetOrderByIdQueryRequest"/>.
/// </summary>
public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, SingleResponse<OrderDto?>>
{
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IUserContextService _userContextService;

    public GetOrderByIdQueryHandler(
        IOrderReadRepository orderReadRepository,
        IUserContextService userContextService)
    {
        _orderReadRepository = orderReadRepository;
        _userContextService = userContextService;
    }

    public async Task<SingleResponse<OrderDto?>> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<OrderDto?>();

        var includes = new List<Expression<Func<Order, object>>>
        {
            p => p.Basket!
        };

        var order = await _orderReadRepository.GetByIdAsync(request.Id, include: includes)
            ?? throw new NotFoundException(nameof(Order), request.Id);

        if (!_userContextService.IsAdminOrSelf(order.Basket!.CustomerId))
            throw new ForbiddenException();

        if (!request.IncludeBasket)
            order.Basket = null;

        response.SetData(order.ToDto());

        return response;
    }
}