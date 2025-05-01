using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Application.Mappings.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using MediatR;
using System.Linq.Expressions;

namespace CleanArchitectureTemplate.Application.Features.Baskets.Queries.GetBasketById;

/// <summary>
/// Handles the <see cref="GetBasketByIdQueryRequest"/>.
/// </summary>
public class GetBasketByIdQueryHandler : IRequestHandler<GetBasketByIdQueryRequest, SingleResponse<BasketDto?>>
{
    private readonly IBasketReadRepository _basketReadRepository;
    private readonly IUserContextService _userContextService;

    public GetBasketByIdQueryHandler(
        IBasketReadRepository basketReadRepository,
        IUserContextService userContextService)
    {
        _basketReadRepository = basketReadRepository;
        _userContextService = userContextService;
    }

    public async Task<SingleResponse<BasketDto?>> Handle(GetBasketByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<BasketDto?>();
        var includes = new List<string>();

        if (request.IncludeBasketItems)
        {
            includes.Add(nameof(Basket.BasketItems));
        }

        var basket = await _basketReadRepository.GetByIdAsync(request.Id, includePaths: includes)
            ?? throw new NotFoundException(nameof(Basket), request.Id);

        if (!_userContextService.IsAdminOrSelf(basket.CustomerId))
            throw new ForbiddenException();

        response.SetData(basket.ToDto());

        return response;
    }
}