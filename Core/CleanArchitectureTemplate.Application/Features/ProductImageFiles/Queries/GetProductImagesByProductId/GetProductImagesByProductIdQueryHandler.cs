using CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Files;
using CleanArchitectureTemplate.Application.Mappings.Files;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.ProductImageFiles.Queries.GetProductImagesByProductId;

/// <summary>
/// Handles the <see cref="GetProductImagesByProductIdQueryRequest"/>.
/// </summary>
public class GetProductImagesByProductIdQueryHandler : IRequestHandler<GetProductImagesByProductIdQueryRequest, PagedResponse<ProductImageFileDto?>>
{
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;

    public GetProductImagesByProductIdQueryHandler(IProductImageFileReadRepository productImageFileReadRepository)
    {
        _productImageFileReadRepository = productImageFileReadRepository;
    }

    public async Task<PagedResponse<ProductImageFileDto?>> Handle(GetProductImagesByProductIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new PagedResponse<ProductImageFileDto?>();

        var productImages = await _productImageFileReadRepository.GetAllPaginatedAsync(pi => pi.ProductId == request.ProductId);
        response.SetData(productImages.Select(pi => pi.ToDto()), request.Pagination?.Page, request.Pagination?.Size);

        return response;
    }
}