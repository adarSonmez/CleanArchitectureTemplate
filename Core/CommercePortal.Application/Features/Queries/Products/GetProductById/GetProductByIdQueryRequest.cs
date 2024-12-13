using MediatR;

namespace CommercePortal.Application.Features.Queries.Products.GetProductById;

/// <summary>
/// Represents the request for getting a product by its ID.
/// </summary>
public record GetProductByIdQueryRequest(Guid Id) : IRequest<GetProductByIdQueryResponse>;