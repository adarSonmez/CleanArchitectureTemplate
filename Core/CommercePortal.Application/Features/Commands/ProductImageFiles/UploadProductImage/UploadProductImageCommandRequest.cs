using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommercePortal.Application.Features.Commands.ProductImageFiles.UploadProductImage;

/// <summary>
/// Represents the request for uploading a product image.
/// </summary>
/// <param name="Folder">The folder name.</param>
/// <param name="ProductId">The identifier of the product.</param>
/// <param name="File">The file to upload.</param>
public record class UploadProductImageCommandRequest
(
    string Folder,
    Guid ProductId,
    [FromForm] IFormFile File
) : IRequest<UploadProductImageCommandResponse>;