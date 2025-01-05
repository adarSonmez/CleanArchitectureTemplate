using CommercePortal.Application.Common.Responses;
using CommercePortal.Application.Dtos.Files;
using CommercePortal.Domain.Entities.Files;
using MediatR;

namespace CommercePortal.Application.Features.ProductImageFiles.Queries.GetProductImageById;

/// <summary>
/// Represents the request for getting a <see cref="ProductImageFile"/> by its ID.
/// </summary>
/// <param name="Id">The unique identifier of the product image.</param>
public record GetProductImageByIdQueryRequest(Guid Id) : IRequest<SingleResponse<ProductImageFileDto>>;