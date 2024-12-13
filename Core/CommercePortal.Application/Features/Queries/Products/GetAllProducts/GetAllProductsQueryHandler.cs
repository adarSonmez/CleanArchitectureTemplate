using CommercePortal.Application.Repositories.Products;
using MediatR;

namespace CommercePortal.Application.Features.Queries.Products.GetAllProducts;

/// <summary>
/// Handles the <see cref="GetAllProductsQueryRequest"/>.
/// </summary>
public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, GetAllProductsQueryResponse>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetAllProductsQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<GetAllProductsQueryResponse> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var products = await _productReadRepository.GetAllPaginatedAsync(pagination: request.Pagination)!;

        return new GetAllProductsQueryResponse
        (
            Count: products?.Count() ?? 0,
            Products: products ?? []
        );
    }
}