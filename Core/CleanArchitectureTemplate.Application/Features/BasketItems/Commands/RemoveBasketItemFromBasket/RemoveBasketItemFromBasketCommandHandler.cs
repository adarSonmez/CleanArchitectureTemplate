using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Application.Mappings.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.BasketItems.Commands.RemoveBasketItemFromBasket;

/// <summary>
/// Handles the <see cref="RemoveBasketItemFromBasketCommandRequest"/>.
/// </summary>
public class RemoveBasketItemFromBasketCommandHandler : IRequestHandler<RemoveBasketItemFromBasketCommandRequest, SingleResponse<BasketDto?>>
{
    private readonly IBasketReadRepository _basketReadRepository;
    private readonly IBasketItemWriteRepository _basketItemWriteRepository;
    private readonly IUserContextService _userContextService;

    public RemoveBasketItemFromBasketCommandHandler(
        IBasketReadRepository basketReadRepository,
        IBasketItemWriteRepository basketItemWriteRepository,
        IUserContextService userContextService)
    {
        _basketReadRepository = basketReadRepository;
        _basketItemWriteRepository = basketItemWriteRepository;
        _userContextService = userContextService;
    }

    public async Task<SingleResponse<BasketDto?>> Handle(RemoveBasketItemFromBasketCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<BasketDto?>();
        var includePaths = new List<string>
        {
            nameof(Basket.BasketItems)
        };

        var basket = await _basketReadRepository.GetByIdAsync(request.BasketId, throwIfNotFound: true, includePaths: includePaths);

        if (!_userContextService.IsAdminOrSelf(basket!.CustomerId))
            throw new ForbiddenException();

        var basketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == request.ProductId)
            ?? throw new NotFoundException(nameof(BasketItem), request.ProductId);

        if (request.Clear)
        {
            await _basketItemWriteRepository.HardDeleteAsync(basketItem);
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

        response.SetData(basket.ToDto());

        return response;
    }
}