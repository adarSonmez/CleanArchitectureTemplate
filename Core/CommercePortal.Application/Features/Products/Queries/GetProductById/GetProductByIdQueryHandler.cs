using AutoMapper;
using CommercePortal.Application.Abstractions.Repositories.Marketing;
using CommercePortal.Application.Common.Responses;
using CommercePortal.Application.Dtos.Marketing;
using CommercePortal.Domain.Common;
using MediatR;

namespace CommercePortal.Application.Features.Products.Queries.GetProductById;

/// <summary>
/// Handles the <see cref="GetProductByIdQueryRequest"/>.
/// </summary>
public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, SingleResponse<ProductDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductReadRepository _productReadRepository;

    public GetProductByIdQueryHandler(IMapper mapper, IProductReadRepository productReadRepository)
    {
        _mapper = mapper;
        _productReadRepository = productReadRepository;
    }

    public async Task<SingleResponse<ProductDto>> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<ProductDto>();
        try
        {
            var product = await _productReadRepository.GetAsync(p => p.Id == request.Id);
            BusinessRules.Run(("PRD599445", BusinessRules.CheckEntityNull(product)));

            response.SetData(_mapper.Map<ProductDto>(product));
        }
        catch (Exception ex)
        {
            response.AddError("PRD380171", ex.Message);
        }
        return response;
    }
}