using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Files;
using CleanArchitectureTemplate.Domain.Entities.Files;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.UploadSecondaryProductImages;

/// <summary>
/// Represents the request for uploading the secondary <see cref="ProductImageFile"/> of a product.
/// </summary>
/// <param name="Folder">The folder name.</param>
/// <param name="ProductId">The identifier of the product.</param>
/// <param name="Files">The files to upload.</param>
public record class UploadSecondaryProductImagesCommandRequest
(
    string Folder,
    Guid ProductId,
    IFormFileCollection Files
) : IRequest<PagedResponse<ProductImageFileDto?>>;