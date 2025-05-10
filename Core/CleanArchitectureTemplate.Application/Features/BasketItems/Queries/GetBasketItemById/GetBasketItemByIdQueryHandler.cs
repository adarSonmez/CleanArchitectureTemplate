using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Application.Mappings.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.BasketItems.Queries.GetBasketItemById;

/// <summary>
/// Handles the <see cref="GetBasketItemByIdQueryRequest"/>.
/// </summary>
public class GetBasketItemByIdQueryHandler : IRequestHandler<GetBasketItemByIdQueryRequest, SingleResponse<BasketItemDto?>>
{
    private readonly IBasketItemReadRepository _basketItemReadRepository;
    private readonly IBasketReadRepository _basketReadRepository;
    private readonly IUserContextService _userContextService;

    public GetBasketItemByIdQueryHandler(
        IBasketItemReadRepository basketItemReadRepository,
        IBasketReadRepository basketReadRepository,
        IUserContextService userContextService)
    {
        _basketItemReadRepository = basketItemReadRepository;
        _basketReadRepository = basketReadRepository;
        _userContextService = userContextService;
    }

    public async Task<SingleResponse<BasketItemDto?>> Handle(GetBasketItemByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<BasketItemDto?>();

        var basketItem = await _basketItemReadRepository.GetByIdAsync(request.Id, throwIfNotFound: true);
        var basket = await _basketReadRepository.GetByIdAsync(basketItem!.BasketId, throwIfNotFound: true);

        if (!_userContextService.IsAdminOrSelf(basket!.CustomerId))
            throw new ForbiddenException();

        response.SetData(basketItem.ToDto());

        return response;
    }
}