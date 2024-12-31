using CommercePortal.Application.Common.Responses;
using CommercePortal.Domain.ValueObjects;
using MediatR;

namespace CommercePortal.Application.Features.Products.Commands.CreateProduct;

/// <summary>
/// Represents the request for creating a product.
/// </summary>
/// <param name="Name">The name of the product.</param>
/// <param name="Description">The description of the product.</param>
/// <param name="Stock">The stock of the product.</param>
/// <param name="DiscountRate">The discount rate of the product.</param>
/// <param name="StandardPrice">The standard price (non-discounted) of the product.</param>
/// <param name="CategoryIds">The identifiers of the categories of the product.</param>
/// <param name="ImageFileIds">The identifiers of the image files of the product.</param>
public record CreateProductCommandRequest
(
    string Name,
    string? Description,
    int? Stock,
    decimal? DiscountRate,
    Money StandardPrice,
    IList<Guid> CategoryIds,
    IList<Guid>? ImageFileIds
) : IRequest<SingleResponse<Guid>>;