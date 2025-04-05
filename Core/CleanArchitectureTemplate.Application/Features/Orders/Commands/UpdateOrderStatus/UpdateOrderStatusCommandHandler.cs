using CleanArchitectureTemplate.Application.Abstractions.Repositories.Ordering;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Ordering;
using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Application.Features.Orders.Commands.CreateOrderFromBasket;
using CleanArchitectureTemplate.Application.Mappings.Ordering;
using CleanArchitectureTemplate.Domain.Entities.Ordering;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Orders.Commands.UpdateOrderStatus;

/// <summary>
/// Handles the <see cref="CreateOrderFromBasketCommandRequest"/>.
/// </summary>
public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommandRequest, SingleResponse<OrderDto?>>
{
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IOrderWriteRepository _orderWriteRepository;

    public UpdateOrderStatusCommandHandler(IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository)
    {
        _orderReadRepository = orderReadRepository;
        _orderWriteRepository = orderWriteRepository;
    }

    public async Task<SingleResponse<OrderDto?>> Handle(UpdateOrderStatusCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<OrderDto?>();

        var order = await _orderReadRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Order), request.Id);

        order.Status = request.Status;
        await _orderWriteRepository.UpdateAsync(order);

        response.SetData(order.ToDto());

        return response;
    }
}