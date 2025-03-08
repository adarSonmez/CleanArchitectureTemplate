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
[Cache(120, 360)]
public record GetProductImageByIdQueryRequest(Guid Id) : IRequest<SingleResponse<ProductImageFileDto?>>;