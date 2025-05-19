using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Application.Mappings.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using MediatR;
using System.Linq.Expressions;

namespace CleanArchitectureTemplate.Application.Features.BasketItems.Commands.AddBasketItemToBasket;

/// <summary>
/// Handles the <see cref="AddBasketItemToBasketCommandRequest"/>.
/// </summary>
public class AddBasketItemToBasketCommandHandler : IRequestHandler<AddBasketItemToBasketCommandRequest, SingleResponse<BasketDto?>>
{
    private readonly IBasketReadRepository _basketReadRepository;
    private readonly IBasketWriteRepository _basketWriteRepository;
    private readonly IBasketItemWriteRepository _basketItemWriteRepository;
    private readonly IBasketItemReadRepository _basketItemReadRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IUserContextService _userContextService;

    public AddBasketItemToBasketCommandHandler(
        IBasketReadRepository basketReadRepository,
        IBasketWriteRepository basketWriteRepository,
        IBasketItemWriteRepository basketItemWriteRepository,
        IBasketItemReadRepository basketItemReadRepository,
        IProductReadRepository productReadRepository,
        IUserContextService userContextService)
    {
        _basketReadRepository = basketReadRepository;
        _basketWriteRepository = basketWriteRepository;
        _basketItemWriteRepository = basketItemWriteRepository;
        _basketItemReadRepository = basketItemReadRepository;
        _productReadRepository = productReadRepository;
        _userContextService = userContextService;
    }

    public async Task<SingleResponse<BasketDto?>> Handle(AddBasketItemToBasketCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<BasketDto?>();

        var includes = new List<string>
        {
            nameof(Basket.BasketItems)
        };

        var basket = await _basketReadRepository.GetByIdAsync(request.BasketId, throwIfNotFound: true, includePaths: includes);

        if (!_userContextService.IsAdminOrSelf(basket!.CustomerId))
            throw new ForbiddenException();

        var product = await _productReadRepository.GetByIdAsync(request.ProductId, throwIfNotFound: true);

        var basketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == request.ProductId);
        if (basketItem == null)
        {
            basketItem = request.ToEntity();
            await _basketItemWriteRepository.AddAsync(basketItem);
        }
        else
        {
            basketItem.Quantity += request.Quantity;
            await _basketItemWriteRepository.UpdateAsync(basketItem);
        }

        response.SetData(basket.ToDto());

        return response;
    }
}