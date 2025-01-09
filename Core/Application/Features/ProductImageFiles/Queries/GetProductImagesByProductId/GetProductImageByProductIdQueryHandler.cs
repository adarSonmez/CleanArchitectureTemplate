using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Files;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.ProductImageFiles.Queries.GetProductImagesByProductId;

/// <summary>
/// Handles the <see cref="GetProductImageByProductIdQueryRequest"/>.
/// </summary>
public class GetProductImageByProductIdQueryHandler : IRequestHandler<GetProductImageByProductIdQueryRequest, PagedResponse<ProductImageFileDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;

    public GetProductImageByProductIdQueryHandler(IMapper mapper, IProductImageFileReadRepository productImageFileReadRepository)
    {
        _mapper = mapper;
        _productImageFileReadRepository = productImageFileReadRepository;
    }

    public async Task<PagedResponse<ProductImageFileDto>> Handle(GetProductImageByProductIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new PagedResponse<ProductImageFileDto>();
        try
        {
            var products = await _productImageFileReadRepository.GetAllPaginatedAsync(pi => pi.ProductId == request.ProductId);
            response.SetData(_mapper.Map<IEnumerable<ProductImageFileDto>>(products));
        }
        catch (Exception ex)
        {
            response.AddError("PIF630102", ex.Message);
        }

        return response;
    }
}