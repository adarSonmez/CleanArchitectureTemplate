using AutoMapper;
using CommercePortal.Application.Abstractions.Repositories.Files;
using CommercePortal.Application.Abstractions.Repositories.Marketing;
using CommercePortal.Application.Common.Responses;
using CommercePortal.Domain.Entities.Marketing;
using MediatR;

namespace CommercePortal.Application.Features.Products.Commands.CreateProduct;

/// <summary>
/// Handles the <see cref="CreateProductCommandRequest"/>.
/// </summary>
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, SingleResponse<Guid>>
{
    private readonly IMapper _mapper;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;
    private readonly ICategoryReadRepository _categoryReadRepository;

    public CreateProductCommandHandler(IMapper mapper, IProductWriteRepository productWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, ICategoryReadRepository categoryReadRepository)
    {
        _mapper = mapper;
        _productWriteRepository = productWriteRepository;
        _productImageFileReadRepository = productImageFileReadRepository;
        _categoryReadRepository = categoryReadRepository;
    }

    public async Task<SingleResponse<Guid>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<Guid>();

        try
        {
            var product = _mapper.Map<Product>(request);
            var categories = await _categoryReadRepository.GetByIdRangeAsync(request.CategoryIds);

            product.Categories = categories.ToList();

            if (request.ImageFileIds != null)
            {
                var imageFiles = await _productImageFileReadRepository.GetByIdRangeAsync(request.ImageFileIds);
                product.ProductImageFiles = imageFiles.ToList();
            }

            var addedProduct = await _productWriteRepository.AddAsync(product);
            response.SetData(addedProduct.Id);
        }
        catch (Exception ex)
        {
            response.AddError("PRD300483", ex.Message);
        }

        return response;
    }
}