using CommercePortal.Domain.Entities;

namespace CommercePortal.Application.Features.Queries.Products.GetProductById;

/// <summary>
/// Represents the response of the <see cref="GetProductByIdQueryRequest"/>.
/// </summary>
/// <param name="Id">The product identifier.</param>
/// <param name="Name">The product name.</param>
/// <param name="Description">The product description.</param>
/// <param name="Price">The product price.</param>
/// <param name="Stock">The product stock.</param>
public record GetProductByIdQueryResponse
(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Stock
);