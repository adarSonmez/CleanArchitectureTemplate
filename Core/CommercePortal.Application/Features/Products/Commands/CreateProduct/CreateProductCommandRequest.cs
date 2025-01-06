using CommercePortal.Application.Common.Responses;
using CommercePortal.Application.Dtos.Marketing;
using CommercePortal.Domain.Entities.Marketing;
using CommercePortal.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommercePortal.Application.Features.Products.Commands.CreateProduct;

/// <summary>
/// Represents the request for creating a <see cref="Product"/>.
/// </summary>
/// <param name="Name">The name of the product.</param>
/// <param name="Description">The description of the product.</param>
/// <param name="Stock">The stock of the product.</param>
/// <param name="DiscountRate">The discount rate of the product.</param>
/// <param name="StandardPrice">The standard price (non-discounted) of the product.</param>
/// <param name="PrimaryProductImage">The primary product image file.</param>
/// <param name="SecondaryProductImages">The secondary product image files.</param>
/// <param name="CategoryIds">The identifiers of the categories of the product.</param>
public record CreateProductCommandRequest
(
    string Name,
    string? Description,
    int? Stock,
    decimal? DiscountRate,
    Money StandardPrice,
    [FromForm] IFormFile PrimaryProductImage,
    [FromForm] IFormFileCollection? SecondaryProductImages,
    IList<Guid> CategoryIds
) : IRequest<SingleResponse<ProductDto>>;