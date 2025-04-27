using CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Files;
using CleanArchitectureTemplate.Application.Mappings.Files;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.ProductImageFiles.Queries.GetProductImagesByFolder;

/// <summary>
/// Handles the <see cref="GetProductImagesByFolderQueryRequest"/>.
/// </summary>
public class GetProductImageByProductIdQueryHandler : IRequestHandler<GetProductImagesByFolderQueryRequest, PagedResponse<ProductImageFileDto?>>
{
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;

    public GetProductImageByProductIdQueryHandler(IProductImageFileReadRepository productImageFileReadRepository)
    {
        _productImageFileReadRepository = productImageFileReadRepository;
    }

    public async Task<PagedResponse<ProductImageFileDto?>> Handle(GetProductImagesByFolderQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new PagedResponse<ProductImageFileDto?>();

        var (data, totalCount) = await _productImageFileReadRepository.GetAllPaginatedAsync(pi => pi.FileDetails!.Folder == request.Folder, include: [pi => pi.FileDetails!]);
        response.SetData(data.Select(pi => pi.ToDto()), totalCount, request.Pagination?.Page, request.Pagination?.Size);

        return response;
    }
}