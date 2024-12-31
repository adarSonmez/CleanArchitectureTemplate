using CommercePortal.Application.Common.Responses;
using MediatR;

namespace CommercePortal.Application.Features.Products.Commands.DeleteProduct;

/// <summary>
/// Represents the request for updating a product.
/// </summary>
/// <param name="Id">The identifier of the product.</param>
public record DeleteProductCommandRequest(Guid Id) : IRequest<SingleResponse<Guid>>;