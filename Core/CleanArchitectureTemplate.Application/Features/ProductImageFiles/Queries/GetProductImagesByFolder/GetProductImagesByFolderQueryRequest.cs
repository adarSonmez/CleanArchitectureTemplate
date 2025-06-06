﻿using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Files;
using CleanArchitectureTemplate.Application.RequestParameters;
using CleanArchitectureTemplate.Domain.Entities.Files;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.ProductImageFiles.Queries.GetProductImagesByFolder;

/// <summary>
/// Represents the request for getting a <see cref="ProductImageFile"/>'s by folder.
/// </summary>
/// <param name="Pagination">The pagination parameters.</param>
/// <param name="Folder">The folder name where the images are stored.</param>
/// <param name="IncludeFileDetails">Indicates whether to include file details in the response.</param>
public record GetProductImagesByFolderQueryRequest(
    Pagination? Pagination,
    string Folder,
    bool IncludeFileDetails
) : IRequest<PagedResponse<ProductImageFileDto?>>;