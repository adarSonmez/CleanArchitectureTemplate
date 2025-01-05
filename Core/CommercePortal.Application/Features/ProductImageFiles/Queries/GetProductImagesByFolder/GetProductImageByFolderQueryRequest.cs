using CommercePortal.Application.Common.Responses;
using CommercePortal.Application.Dtos.Files;
using CommercePortal.Domain.Entities.Files;
using MediatR;

namespace CommercePortal.Application.Features.ProductImageFiles.Queries.GetProductImagesByFolder;

/// <summary>
/// Represents the request for getting a <see cref="ProductImageFile"/>'s by folder.
/// </summary>
/// <param name="Folder">The folder name where the images are stored.</param>
public record GetProductImageByFolderQueryRequest(string Folder) : IRequest<PagedResponse<ProductImageFileDto>>;