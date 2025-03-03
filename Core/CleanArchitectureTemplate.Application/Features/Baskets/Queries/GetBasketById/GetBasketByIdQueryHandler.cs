using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Application.Exceptions;
using MediatR;
using System.Linq.Expressions;

namespace CleanArchitectureTemplate.Application.Features.Baskets.Queries.GetBasketById;

/// <summary>
/// Handles the <see cref="GetBasketByIdQueryRequest"/>.
/// </summary>
public class GetBasketByIdQueryHandler : IRequestHandler<GetBasketByIdQueryRequest, SingleResponse<BasketDto?>>
{
    private readonly IMapper _mapper;
    private readonly IBasketReadRepository _basketReadRepository;
    private readonly IUserContextService _userContextService;

    public GetBasketByIdQueryHandler(
        IMapper mapper,
        IBasketReadRepository basketReadRepository,
        IUserContextService userContextService)
    {
        _mapper = mapper;
        _basketReadRepository = basketReadRepository;
        _userContextService = userContextService;
    }

    public async Task<SingleResponse<BasketDto?>> Handle(GetBasketByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<BasketDto?>();
        var includes = new List<Expression<Func<Basket, object>>>();

        if (request.IncludeBasketItems)
        {
            includes.Add(p => p.BasketItems);
        }

        var basket = await _basketReadRepository.GetByIdAsync(request.Id, include: includes)
            ?? throw new NotFoundException(nameof(Basket), request.Id);

        if (!_userContextService.IsAdminOrSelf(basket.CustomerId))
            throw new ForbiddenException();

        response.SetData(_mapper.Map<BasketDto>(basket));

        return response;
    }
}