using AutoMapper;
using CommercePortal.Application.Abstractions.Repositories.Files;
using CommercePortal.Application.Common.Responses;
using CommercePortal.Application.Dtos.Files;
using MediatR;

namespace CommercePortal.Application.Features.ProductImageFiles.Queries.GetProductImagesByFolder;

/// <summary>
/// Handles the <see cref="GetProductImageByFolderQueryRequest"/>.
/// </summary>
public class GetProductImageByProductIdQueryHandler : IRequestHandler<GetProductImageByFolderQueryRequest, PagedResponse<ProductImageFileDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;

    public GetProductImageByProductIdQueryHandler(IMapper mapper, IProductImageFileReadRepository productImageFileReadRepository)
    {
        _mapper = mapper;
        _productImageFileReadRepository = productImageFileReadRepository;
    }

    public async Task<PagedResponse<ProductImageFileDto>> Handle(GetProductImageByFolderQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new PagedResponse<ProductImageFileDto>();
        try
        {
            var products = await _productImageFileReadRepository.GetAllPaginatedAsync(pi => pi.FileDetails.Folder == request.Folder, include: [pi => pi.FileDetails]);
            response.SetData(_mapper.Map<IEnumerable<ProductImageFileDto>>(products));
        }
        catch (Exception ex)
        {
            response.AddError("PIF518440", ex.Message);
        }

        return response;
    }
}