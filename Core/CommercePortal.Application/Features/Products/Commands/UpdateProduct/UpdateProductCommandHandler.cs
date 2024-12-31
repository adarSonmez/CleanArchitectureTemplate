using AutoMapper;
using CommercePortal.Application.Abstractions.Repositories.Files;
using CommercePortal.Application.Abstractions.Repositories.Marketing;
using CommercePortal.Application.Common.Responses;
using CommercePortal.Application.Dtos.Marketing;
using CommercePortal.Domain.Common;
using MediatR;

namespace CommercePortal.Application.Features.Products.Commands.UpdateProduct;

/// <summary>
/// Handles the <see cref="UpdateProductCommandRequest"/>.
/// </summary>
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, SingleResponse<ProductDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;
    private readonly ICategoryReadRepository _categoryReadRepository;

    public UpdateProductCommandHandler(IMapper mapper, IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IProductImageFileReadRepository productImageFileReadRepository, ICategoryReadRepository categoryReadRepository)
    {
        _mapper = mapper;
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
        _productImageFileReadRepository = productImageFileReadRepository;
        _categoryReadRepository = categoryReadRepository;
    }

    public async Task<SingleResponse<ProductDto>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<ProductDto>();
        try
        {
            var product = await _productReadRepository.GetAsync(p => p.Id == request.Id);
            BusinessRules.Run(("PRD128204", BusinessRules.CheckEntityNull(product)));

            _mapper.Map(request, product);
            if (request.CategoryIds != null)
            {
                var categories = await _categoryReadRepository.GetByIdRangeAsync(request.CategoryIds);
                product!.Categories = categories.ToList();
            }
            if (request.ImageFileIds != null)
            {
                var imageFiles = await _productImageFileReadRepository.GetByIdRangeAsync(request.ImageFileIds);
                product!.ProductImageFiles = imageFiles.ToList();
            }

            var updatedProduct = await _productWriteRepository.UpdateAsync(product!);
            response.SetData(_mapper.Map<ProductDto>(updatedProduct));
        }
        catch (Exception ex)
        {
            response.AddError("PRD913560", ex.Message);
        }
        return response;
    }
}