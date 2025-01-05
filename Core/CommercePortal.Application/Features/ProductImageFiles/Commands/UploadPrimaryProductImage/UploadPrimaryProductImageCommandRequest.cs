using CommercePortal.Application.Common.Responses;
using CommercePortal.Application.Dtos.Files;
using CommercePortal.Domain.Entities.Files;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommercePortal.Application.Features.ProductImageFiles.Commands.UploadPrimaryProductImage;

/// <summary>
/// Represents the request for uploading the primary <see cref="ProductImageFile"/> of a product.
/// </summary>
/// <param name="Folder">The folder name.</param>
/// <param name="ProductId">The identifier of the product.</param>
/// <param name="File">The file to upload.</param>
public record class UploadPrimaryProductImageCommandRequest
(
    string Folder,
    Guid ProductId,
    [FromForm] IFormFile File
) : IRequest<SingleResponse<ProductImageFileDto>>;