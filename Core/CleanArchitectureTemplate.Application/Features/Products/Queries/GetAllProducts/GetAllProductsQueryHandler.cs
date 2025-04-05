using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Application.Mappings.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using MediatR;
using System.Linq.Expressions;

namespace CleanArchitectureTemplate.Application.Features.Products.Queries.GetAllProducts;

/// <summary>
/// Handles the <see cref="GetAllProductsQueryRequest"/>.
/// </summary>
public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, PagedResponse<ProductDto?>>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetAllProductsQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<PagedResponse<ProductDto?>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new PagedResponse<ProductDto?>();

        var includes = new List<Expression<Func<Product, object>>>();

        if (request.IncludeCategories)
        {
            includes.Add(p => p.Categories);
        }
        if (request.IncludeBasketItems)
        {
            includes.Add(p => p.BasketItems);
        }
        if (request.IncludeProductImageFiles)
        {
            includes.Add(p => p.ProductImageFiles);
        }

        var products = await _productReadRepository.GetAllPaginatedAsync(
            pagination: request.Pagination,
            include: includes);

        response.SetData(products.Select(p => p.ToDto()), request.Pagination?.Page, request.Pagination?.Size);

        return response;
    }
}