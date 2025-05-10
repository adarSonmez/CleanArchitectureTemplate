using CleanArchitectureTemplate.Application.Attributes;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Files;
using CleanArchitectureTemplate.Domain.Entities.Files;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.ProductImageFiles.Queries.GetProductImageById;

/// <summary>
/// Represents the request for getting a <see cref="ProductImageFile"/> by its ID.
/// </summary>
/// <param name="Id">The unique identifier of the product image.</param>
/// <param name="IncludeFileDetails">Indicates whether to include file details in the response.</param>
[Cache(120, 360)]
public record GetProductImageByIdQueryRequest
(
    Guid Id,
    bool IncludeFileDetails
) : IRequest<SingleResponse<ProductImageFileDto?>>;