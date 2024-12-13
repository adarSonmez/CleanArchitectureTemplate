using MediatR;
using Microsoft.AspNetCore.Http;

namespace CommercePortal.Application.Features.Commands.ProductImageFiles.UploadProductImage;

/// <summary>
/// Represents the request for uploading a product image.
/// </summary>
/// <param name="Folder">The folder name.</param>
/// <param name="File">The file to upload.</param>
/// <param name="ProductId">The identifier of the product.</param>
public record class UploadProductImageCommandRequest
(
    string Folder,
    IFormFile File,
    Guid ProductId
) : IRequest<UploadProductImageCommandResponse>;