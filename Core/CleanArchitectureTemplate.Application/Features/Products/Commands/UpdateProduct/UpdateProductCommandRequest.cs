using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Marketing;
using CleanArchitectureTemplate.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.Application.Features.Products.Commands.UpdateProduct;

/// <summary>
/// Represents the request for updating a product.
/// </summary>
/// <param name="Id">The identifier of the product.</param>
/// <param name="Name">The name of the product.</param>
/// <param name="Description">The description of the product.</param>
/// <param name="Stock">The stock of the product.</param>
/// <param name="DiscountRate">The discount rate of the product.</param>
/// <param name="StandardPrice">The standard price (non-discounted) of the product.</param>
/// <param name="PrimaryProductImage">The primary product image file.</param>
/// <param name="DeleteExistingSecondaryImages">A flag indicating whether to delete existing secondary images.</param>
/// <param name="SecondaryProductImages">The secondary product image files.</param>
/// <param name="CategoryIds">The identifiers of the categories of the product.</param>

public record UpdateProductCommandRequest
(
    Guid Id,
    string? Name,
    string? Description,
    int? Stock,
    decimal? DiscountRate,
    Money? StandardPrice,
    [FromForm] IFormFile PrimaryProductImage,
    bool DeleteExistingSecondaryImages,
    [FromForm] IFormFileCollection? SecondaryProductImages,
    IList<Guid>? CategoryIds
) : IRequest<SingleResponse<ProductDto?>>;