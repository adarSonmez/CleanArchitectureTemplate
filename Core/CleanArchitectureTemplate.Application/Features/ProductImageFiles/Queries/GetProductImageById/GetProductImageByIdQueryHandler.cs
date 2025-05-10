using CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Files;
using CleanArchitectureTemplate.Domain.Entities.Files;
using MediatR;
using CleanArchitectureTemplate.Application.Mappings.Files;

namespace CleanArchitectureTemplate.Application.Features.ProductImageFiles.Queries.GetProductImageById;

/// <summary>
/// Handles the <see cref="GetProductImageByIdQueryRequest"/>.
/// </summary>
public class GetProductImageByIdQueryHandler : IRequestHandler<GetProductImageByIdQueryRequest, SingleResponse<ProductImageFileDto?>>
{
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;

    public GetProductImageByIdQueryHandler(IProductImageFileReadRepository productImageFileReadRepository)
    {
        _productImageFileReadRepository = productImageFileReadRepository;
    }

    public async Task<SingleResponse<ProductImageFileDto?>> Handle(GetProductImageByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<ProductImageFileDto?>();

        var includes = new List<string>();
        if (request.IncludeFileDetails)
        {
            includes.Add(nameof(ProductImageFile.FileDetails));
        }

        var productImageFile = await _productImageFileReadRepository.GetByIdAsync(request.Id, includePaths: includes, throwIfNotFound: true);

        response.SetData(productImageFile!.ToDto());

        return response;
    }
}