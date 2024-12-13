using MediatR;

namespace CommercePortal.Application.Features.Commands.Products.CreateProduct;

/// <summary>
/// Represents the request for creating a product.
/// </summary>
/// <param name="Name">The name of the product.</param>
/// <param name="Description">The description of the product.</param>
/// <param name="Stock">The stock of the product.</param>
/// <param name="Price">The price of the product.</param>
public record CreateProductCommandRequest
(
    string Name,
    string? Description,
    int? Stock,
    decimal Price
) : IRequest<CreateProductCommandResponse>;