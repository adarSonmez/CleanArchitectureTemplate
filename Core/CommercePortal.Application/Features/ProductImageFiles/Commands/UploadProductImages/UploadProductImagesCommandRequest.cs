using CommercePortal.Application.Common.Responses;
using CommercePortal.Application.Dtos.Files;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommercePortal.Application.Features.ProductImageFiles.Commands.UploadProductImages;

/// <summary>
/// Represents the request for uploading a product image.
/// </summary>
/// <param name="Folder">The folder name.</param>
/// <param name="ProductId">The identifier of the product.</param>
/// <param name="Files">The files to upload.</param>
public record class UploadProductImagesCommandRequest
(
    string Folder,
    Guid ProductId,
    [FromForm] IFormFileCollection Files
) : IRequest<PagedResponse<ProductImageFileDto>>;