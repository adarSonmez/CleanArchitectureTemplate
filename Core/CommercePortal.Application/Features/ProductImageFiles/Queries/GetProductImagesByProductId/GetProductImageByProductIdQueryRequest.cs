using CommercePortal.Application.Common.Responses;
using CommercePortal.Application.Dtos.Files;
using CommercePortal.Domain.Entities.Files;
using MediatR;

namespace CommercePortal.Application.Features.ProductImageFiles.Queries.GetProductImagesByProductId;

/// <summary>
/// Represents the request for getting a <see cref="ProductImageFile"/>'s by its ProductId.
/// </summary>
/// <param name="ProductId">The unique identifier of the product which the image belongs to.</param>
public record GetProductImageByProductIdQueryRequest(Guid ProductId) : IRequest<PagedResponse<ProductImageFileDto>>;