using CommercePortal.Application.RequestParameters;
using MediatR;

namespace CommercePortal.Application.Features.Queries.Products.GetAllProducts;

/// <summary>
/// Represents the request for getting all products.
/// </summary>
/// <param name="Pagination">The pagination parameters.</param>
public record GetAllProductsQueryRequest(Pagination Pagination) : IRequest<GetAllProductsQueryResponse>;