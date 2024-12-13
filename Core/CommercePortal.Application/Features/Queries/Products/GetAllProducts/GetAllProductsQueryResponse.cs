namespace CommercePortal.Application.Features.Queries.Products.GetAllProducts;

/// <summary>
/// Represents the response of the <see cref="GetAllProductsQueryRequest"/>.
/// </summary>
/// <param name="Count">The total number of products.</param>
/// <param name="Products">The products.</param>
public record GetAllProductsQueryResponse(int Count, object Products);