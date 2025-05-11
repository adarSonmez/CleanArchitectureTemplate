using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Files;
using CleanArchitectureTemplate.Domain.Entities.Files;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.UploadPrimaryProductImage;

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
    IFormFile File
) : IRequest<SingleResponse<ProductImageFileDto>>;