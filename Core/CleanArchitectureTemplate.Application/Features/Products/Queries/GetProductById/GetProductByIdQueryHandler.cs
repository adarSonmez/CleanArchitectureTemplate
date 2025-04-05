using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Application.Mappings.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using MediatR;
using System.Linq.Expressions;

namespace CleanArchitectureTemplate.Application.Features.Products.Queries.GetProductById;

/// <summary>
/// Handles the <see cref="GetProductByIdQueryRequest"/>.
/// </summary>
public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, SingleResponse<ProductDto?>>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetProductByIdQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<SingleResponse<ProductDto?>> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<ProductDto?>();
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

        var product = await _productReadRepository.GetByIdAsync(request.Id, include: includes)
        ?? throw new NotFoundException(nameof(Product), request.Id);

        response.SetData(product.ToDto());

        return response;
    }
}