using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using MediatR;
using System.Linq.Expressions;

namespace CleanArchitectureTemplate.Application.Features.Products.Queries.GetAllProducts;

/// <summary>
/// Handles the <see cref="GetAllProductsQueryRequest"/>.
/// </summary>
public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, PagedResponse<ProductDto?>>
{
    private readonly IMapper _mapper;
    private readonly IProductReadRepository _productReadRepository;

    public GetAllProductsQueryHandler(IMapper mapper, IProductReadRepository productReadRepository)
    {
        _mapper = mapper;
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
        if (request.IncludeOrderItems)
        {
            includes.Add(p => p.OrderItems);
        }
        if (request.IncludeProductImageFiles)
        {
            includes.Add(p => p.ProductImageFiles);
        }

        var products = await _productReadRepository.GetAllPaginatedAsync(
            pagination: request.Pagination,
            include: includes);

        response.SetData(_mapper.Map<IEnumerable<ProductDto>>(products), request.Pagination?.Page, request.Pagination?.Size);

        return response;
    }
}