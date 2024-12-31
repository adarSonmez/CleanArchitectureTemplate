using CommercePortal.Application.Common.Responses;
using CommercePortal.Application.Dtos.Marketing;
using CommercePortal.Domain.ValueObjects;
using MediatR;

namespace CommercePortal.Application.Features.Products.Commands.UpdateProduct;

/// <summary>
/// Represents the request for updating a product.
/// </summary>
/// <param name="Id">The identifier of the product.</param>
/// <param name="Name">The name of the product.</param>
/// <param name="Description">The description of the product.</param>
/// <param name="Stock">The stock of the product.</param>
/// <param name="DiscountRate">The discount rate of the product.</param>
/// <param name="StandardPrice">The standard price (non-discounted) of the product.</param>
/// <param name="CategoryIds">The identifiers of the categories of the product.</param>
/// <param name="ImageFileIds">The identifier of the image file of the product.</param>
public record UpdateProductCommandRequest
(
    Guid Id,
    string? Name,
    string? Description,
    int? Stock,
    decimal? DiscountRate,
    Money? StandardPrice,
    IList<Guid>? CategoryIds,
    IList<Guid>? ImageFileIds
) : IRequest<SingleResponse<ProductDto>>;