using CleanArchitectureTemplate.Application.Abstractions.Repositories.Ordering;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Ordering;
using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Application.Mappings.Ordering;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using MediatR;
using System.Linq.Expressions;

namespace CleanArchitectureTemplate.Application.Features.Orders.Commands.CreateOrderFromBasket;

/// <summary>
/// Handles the <see cref="CreateOrderFromBasketCommandRequest"/>.
/// </summary>
public class CreateOrderFromBasketCommandHandler : IRequestHandler<CreateOrderFromBasketCommandRequest, SingleResponse<OrderDto?>>
{
    private readonly IBasketReadRepository _basketReadRepository;
    private readonly IBasketWriteRepository _basketWriteRepository;
    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly IUserContextService _userContextService;

    public CreateOrderFromBasketCommandHandler(
        IBasketReadRepository basketReadRepository,
        IBasketWriteRepository basketWriteRepository,
        IOrderWriteRepository orderWriteRepository,
        IUserContextService userContextService)
    {
        _basketReadRepository = basketReadRepository;
        _basketWriteRepository = basketWriteRepository;
        _orderWriteRepository = orderWriteRepository;
        _userContextService = userContextService;
    }

    public async Task<SingleResponse<OrderDto?>> Handle(CreateOrderFromBasketCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<OrderDto?>();
        var includes = new List<Expression<Func<Basket, object>>>
        {
            p => p.BasketItems
        };

        var basket = await _basketReadRepository.GetByIdAsync(request.BasketId, include: includes)
            ?? throw new NotFoundException(nameof(Basket), request.BasketId);

        if (!_userContextService.IsAdminOrSelf(basket.CustomerId))
            throw new ForbiddenException();

        if (basket.BasketItems == null || basket.BasketItems.Count == 0)
        {
            throw new ValidationFailedException("Cannot create an order from an empty basket.");
        }

        var order = request.ToEntity();
        await _orderWriteRepository.AddAsync(order);

        basket.Ordered = true;
        await _basketWriteRepository.UpdateAsync(basket);

        var newBasket = new Basket { CustomerId = basket.CustomerId };
        await _basketWriteRepository.AddAsync(newBasket);

        response.SetData(order.ToDto());

        return response;
    }
}