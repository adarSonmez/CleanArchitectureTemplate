using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Files;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.ProductImageFiles.Queries.GetProductImagesByFolder;

/// <summary>
/// Handles the <see cref="GetProductImagesByFolderQueryRequest"/>.
/// </summary>
public class GetProductImageByProductIdQueryHandler : IRequestHandler<GetProductImagesByFolderQueryRequest, PagedResponse<ProductImageFileDto?>>
{
    private readonly IMapper _mapper;
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;

    public GetProductImageByProductIdQueryHandler(IMapper mapper, IProductImageFileReadRepository productImageFileReadRepository)
    {
        _mapper = mapper;
        _productImageFileReadRepository = productImageFileReadRepository;
    }

    public async Task<PagedResponse<ProductImageFileDto?>> Handle(GetProductImagesByFolderQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new PagedResponse<ProductImageFileDto?>();

        var productImages = await _productImageFileReadRepository.GetAllPaginatedAsync(pi => pi.FileDetails.Folder == request.Folder, include: [pi => pi.FileDetails]);
        response.SetData(_mapper.Map<IEnumerable<ProductImageFileDto>>(productImages));

        return response;
    }
}