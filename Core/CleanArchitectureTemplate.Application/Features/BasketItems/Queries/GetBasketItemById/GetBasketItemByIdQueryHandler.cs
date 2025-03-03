using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Application.Exceptions;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.BasketItems.Queries.GetBasketItemById;

/// <summary>
/// Handles the <see cref="GetBasketItemByIdQueryRequest"/>.
/// </summary>
public class GetBasketItemByIdQueryHandler : IRequestHandler<GetBasketItemByIdQueryRequest, SingleResponse<BasketItemDto?>>
{
    private readonly IMapper _mapper;
    private readonly IBasketItemReadRepository _basketItemReadRepository;
    private readonly IBasketReadRepository _basketReadRepository;
    private readonly IUserContextService _userContextService;

    public GetBasketItemByIdQueryHandler(
        IMapper mapper,
        IBasketItemReadRepository basketItemReadRepository,
        IBasketReadRepository basketReadRepository,
        IUserContextService userContextService)
    {
        _mapper = mapper;
        _basketItemReadRepository = basketItemReadRepository;
        _basketReadRepository = basketReadRepository;
        _userContextService = userContextService;
    }

    public async Task<SingleResponse<BasketItemDto?>> Handle(GetBasketItemByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<BasketItemDto?>();

        var basketItem = await _basketItemReadRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(BasketItem), request.Id);

        var basket = await _basketReadRepository.GetByIdAsync(basketItem.BasketId)
            ?? throw new NotFoundException(nameof(Basket), basketItem.BasketId);

        if (!_userContextService.IsAdminOrSelf(basket.CustomerId))
            throw new ForbiddenException();

        response.SetData(_mapper.Map<BasketItemDto>(basketItem));

        return response;
    }
}