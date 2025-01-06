using AutoMapper;
using CommercePortal.Application.Abstractions.Repositories.Marketing;
using CommercePortal.Application.Common.Responses;
using CommercePortal.Application.Dtos.Marketing;
using CommercePortal.Application.Features.ProductImageFiles.Commands.UploadPrimaryProductImage;
using CommercePortal.Application.Features.ProductImageFiles.Commands.UploadSecondaryProductImages;
using CommercePortal.Domain.Constants.StringContants;
using CommercePortal.Domain.Entities.Marketing;
using MediatR;

namespace CommercePortal.Application.Features.Products.Commands.CreateProduct;

/// <summary>
/// Handles the <see cref="CreateProductCommandRequest"/>.
/// </summary>
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, SingleResponse<ProductDto>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly ICategoryReadRepository _categoryReadRepository;

    public CreateProductCommandHandler(IMediator mediator, IMapper mapper, IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, ICategoryReadRepository categoryReadRepository)
    {
        _mediator = mediator;
        _mapper = mapper;
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _categoryReadRepository = categoryReadRepository;
    }

    public async Task<SingleResponse<ProductDto>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<ProductDto>();

        try
        {
            var product = _mapper.Map<Product>(request);
            var categories = await _categoryReadRepository.GetByIdRangeAsync(request.CategoryIds);

            product.Categories = categories.ToList();

            if (request.PrimaryProductImage != null)
            {
                var uploadFileCommand = new UploadPrimaryProductImageCommandRequest(PathConstants.DefaultProductImagesPath, product.Id, request.PrimaryProductImage);
                var uploadFileResponse = await _mediator.Send(uploadFileCommand, cancellationToken);
                if (!uploadFileResponse.IsSuccessful)
                {
                    response.AddError("PRD898336", uploadFileResponse.Messages.FirstOrDefault()?.Message ?? "Error while uploading primary image.");
                    return response;
                }
            }

            if (request.SecondaryProductImages != null && request.SecondaryProductImages.Count > 0)
            {
                var uploadFilesCommand = new UploadSecondaryProductImagesCommandRequest(PathConstants.DefaultProductImagesPath, product.Id, request.SecondaryProductImages);
                var uploadFilesResponse = await _mediator.Send(uploadFilesCommand, cancellationToken);
                if (!uploadFilesResponse.IsSuccessful)
                {
                    response.AddError("PRD680515", uploadFilesResponse.Messages.FirstOrDefault()?.Message ?? "Error while uploading secondary images.");
                    return response;
                }
            }

            var addedProduct = await _productWriteRepository.AddAsync(product);
            var detailedProduct = await _productReadRepository.GetByIdAsync(addedProduct.Id, include: [product => product.Categories, product => product.ProductImageFiles]);

            response.SetData(_mapper.Map<ProductDto>(detailedProduct));
        }
        catch (Exception ex)
        {
            response.AddError("PRD300483", ex.Message);
        }

        return response;
    }
}