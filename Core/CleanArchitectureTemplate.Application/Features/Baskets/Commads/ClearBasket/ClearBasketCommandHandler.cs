using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Baskets.Commads.ClearBasket;

/// <summary>
/// Handles the <see cref="ClearBasketCommandRequest"/>.
/// </summary>
public class ClearBasketCommandHandler : IRequestHandler<ClearBasketCommandRequest, SingleResponse<BasketDto?>>
{
    private readonly IMapper _mapper;
    private readonly IBasketReadRepository _basketReadRepository;
    private readonly IBasketItemWriteRepository _basketItemWriteRepository;

    public ClearBasketCommandHandler(IMapper mapper, IBasketReadRepository basketReadRepository, IBasketItemWriteRepository basketItemWriteRepository)
    {
        _mapper = mapper;
        _basketReadRepository = basketReadRepository;
        _basketItemWriteRepository = basketItemWriteRepository;
    }

    public async Task<SingleResponse<BasketDto?>> Handle(ClearBasketCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<BasketDto?>();

        var basket = await _basketReadRepository.GetByIdAsync(request.BasketId)
            ?? throw new NotFoundException(nameof(Basket), request.BasketId);

        foreach (var basketItem in basket.BasketItems)
            await _basketItemWriteRepository.SoftDeleteAsync(basketItem);

        response.SetData(_mapper.Map<BasketDto>(basket));
        return response;
    }
}