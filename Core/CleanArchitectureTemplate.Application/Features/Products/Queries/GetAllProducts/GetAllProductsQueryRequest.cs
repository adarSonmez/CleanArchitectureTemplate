using CleanArchitectureTemplate.Application.Attributes;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Application.RequestParameters;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Products.Queries.GetAllProducts;

/// <summary>
/// Represents the request for getting all products.
/// </summary>
/// <param name="Pagination">The pagination parameters.</param>
/// <param name="IncludeCategories">A flag to include categories.</param>
/// <param name="IncludeProductImageFiles">A flag to include product image files.</param>
public record GetAllProductsQueryRequest
(
    Pagination? Pagination,
    bool IncludeCategories,
    bool IncludeProductImageFiles
) : IRequest<PagedResponse<ProductDto?>>;