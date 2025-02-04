using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Application.Exceptions;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.BasketItems.Commands.RemoveBasketItemFromBasket;

/// <summary>
/// Handles the <see cref="RemoveBasketItemFromBasketCommandRequest"/>.
/// </summary>
public class RemoveBasketItemFromBasketCommandHandler : IRequestHandler<RemoveBasketItemFromBasketCommandRequest, SingleResponse<BasketDto?>>
{
    private readonly IMapper _mapper;
    private readonly IBasketReadRepository _basketReadRepository;
    private readonly IBasketItemWriteRepository _basketItemWriteRepository;

    public RemoveBasketItemFromBasketCommandHandler(IMapper mapper, IBasketReadRepository basketReadRepository, IBasketItemWriteRepository basketItemWriteRepository)
    {
        _mapper = mapper;
        _basketReadRepository = basketReadRepository;
        _basketItemWriteRepository = basketItemWriteRepository;
    }

    public async Task<SingleResponse<BasketDto?>> Handle(RemoveBasketItemFromBasketCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<BasketDto?>();

        var basket = await _basketReadRepository.GetByIdAsync(request.BasketId)
            ?? throw new NotFoundException(nameof(Basket), request.BasketId);

        var basketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == request.ProductId)
            ?? throw new NotFoundException(nameof(BasketItem), request.ProductId);

        if (request.Clear)
        {
            await _basketItemWriteRepository.SoftDeleteAsync(basketItem);
        }
        else
        {
            if (request.Quantity >= basketItem.Quantity)
            {
                throw new ValidationFailedException(nameof(request.Quantity), "Quantity to remove is greater than the quantity in the basket.");
            }
            else
            {
                basketItem.Quantity -= request.Quantity;
                await _basketItemWriteRepository.UpdateAsync(basketItem);
            }
        }

        response.SetData(_mapper.Map<BasketDto>(basket));

        return response;
    }
}