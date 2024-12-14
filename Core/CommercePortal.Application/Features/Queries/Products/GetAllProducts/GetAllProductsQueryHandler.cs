using CommercePortal.Application.Repositories.Products;
using CommercePortal.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

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
        var includes = new List<Expression<Func<Product, object>>>
        {
            product => product.ProductImageFiles,
        };

        var products = await _productReadRepository.GetAllPaginatedAsync(pagination: request.Pagination, include: includes);

        return new GetAllProductsQueryResponse
        (
            Count: products?.Count() ?? 0,
            Products: products == null ? new List<Product>() : products.Select(product => new
            {
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Stock,
                ProductImageFiles = product.ProductImageFiles.Select(productImageFile => new
                {
                    productImageFile.Id,
                    productImageFile.Name,
                    productImageFile.Folder,
                    productImageFile.StorageName
                }).ToList()
            }).ToList()
        );
    }
}