using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Marketing;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Products.Queries.GetProductById;

/// <summary>
/// Represents the request for getting a product by its ID.
/// </summary>
/// <param name="Id">The product ID.</param>
/// <param name="IncludeCategories">A flag to include categories.</param>
/// <param name="IncludeOrderItems">A flag to include order items.</param>
/// <param name="IncludeProductImageFiles">A flag to include product image files.</param>
public record GetProductByIdQueryRequest
(
    Guid Id,
    bool IncludeCategories,
    bool IncludeOrderItems,
    bool IncludeProductImageFiles
) : IRequest<SingleResponse<ProductDto?>>;