using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Application.Mappings.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using MediatR;
using System.Linq.Expressions;

namespace CleanArchitectureTemplate.Application.Features.Baskets.Queries.GetAllBaskets;

/// <summary>
/// Handles the <see cref="GetAllBasketsQueryRequest"/>.
/// </summary>
public class GetAllBasketsQueryHandler : IRequestHandler<GetAllBasketsQueryRequest, PagedResponse<BasketDto>>
{
    private readonly IBasketReadRepository _basketReadRepository;

    public GetAllBasketsQueryHandler(IBasketReadRepository basketReadRepository)
    {
        _basketReadRepository = basketReadRepository;
    }

    public async Task<PagedResponse<BasketDto>> Handle(GetAllBasketsQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new PagedResponse<BasketDto>();

        var includes = new List<Expression<Func<Basket, object>>>();
        if (request.IncludeBasketItems)
        {
            includes.Add(p => p.BasketItems);
        }

        // Fetch paginated baskets from the repository
        var (data, totalCount) = await _basketReadRepository.GetAllPaginatedAsync(
            pagination: request.Pagination,
            include: includes
        );

        response.SetData(
            data.Select(b => b.ToDto()),
            totalCount,
            request.Pagination?.Page,
            request.Pagination?.Size
        );

        return response;
    }
}