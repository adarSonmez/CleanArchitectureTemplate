using MediatR;

namespace CommercePortal.Application.Features.Commands.Products.UpdateProduct;

/// <summary>
/// Represents the request for updating a product.
/// </summary>
/// <param name="Id">The identifier of the product.</param>
/// <param name="Name">The name of the product.</param>
/// <param name="Description">The description of the product.</param>
/// <param name="Stock">The stock of the product.</param>
/// <param name="Price">The price of the product.</param>
public record UpdateProductCommandRequest
(
    Guid Id,
    string? Name,
    string? Description,
    int? Stock,
    decimal? Price
) : IRequest<UpdateProductCommandResponse>;