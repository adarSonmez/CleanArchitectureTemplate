using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
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

    public GetBasketItemByIdQueryHandler(IMapper mapper, IBasketItemReadRepository basketItemReadRepository)
    {
        _mapper = mapper;
        _basketItemReadRepository = basketItemReadRepository;
    }

    public async Task<SingleResponse<BasketItemDto?>> Handle(GetBasketItemByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<BasketItemDto?>();

        var basketItem = await _basketItemReadRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(BasketItem), request.Id);

        response.SetData(_mapper.Map<BasketItemDto>(basketItem));

        return response;
    }
}