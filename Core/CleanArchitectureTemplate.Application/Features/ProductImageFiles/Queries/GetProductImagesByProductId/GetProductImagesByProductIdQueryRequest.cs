using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Files;
using CleanArchitectureTemplate.Domain.Entities.Files;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.ProductImageFiles.Queries.GetProductImagesByProductId;

/// <summary>
/// Represents the request for getting a <see cref="ProductImageFile"/>'s by its ProductId.
/// </summary>
/// <param name="ProductId">The unique identifier of the product which the image belongs to.</param>
public record GetProductImagesByProductIdQueryRequest(Guid ProductId) : IRequest<PagedResponse<ProductImageFileDto?>>;