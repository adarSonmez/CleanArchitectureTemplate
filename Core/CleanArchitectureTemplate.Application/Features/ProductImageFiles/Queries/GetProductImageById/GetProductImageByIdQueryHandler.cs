using CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Files;
using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Application.Exceptions;
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

        var productImageFile = await _productImageFileReadRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(ProductImageFile), request.Id);

        response.SetData(productImageFile.ToDto());

        return response;
    }
}