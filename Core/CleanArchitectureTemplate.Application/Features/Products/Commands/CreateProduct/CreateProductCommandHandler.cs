using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.Hubs;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.UploadPrimaryProductImage;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.UploadSecondaryProductImages;
using CleanArchitectureTemplate.Domain.Constants.StringContants;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Domain.Exceptions;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Products.Commands.CreateProduct;

/// <summary>
/// Handles the <see cref="CreateProductCommandRequest"/>.
/// </summary>
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, SingleResponse<ProductDto?>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly ICategoryReadRepository _categoryReadRepository;
    private readonly IProductHubService _productHubService;

    public CreateProductCommandHandler(IMediator mediator, IMapper mapper, IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, ICategoryReadRepository categoryReadRepository, IProductHubService productHubService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _categoryReadRepository = categoryReadRepository;
        _productHubService = productHubService;
    }

    public async Task<SingleResponse<ProductDto?>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<ProductDto?>();

        var product = _mapper.Map<Product>(request);
        var categories = await _categoryReadRepository.GetByIdRangeAsync(request.CategoryIds);

        product.Categories = categories.ToList();

        if (request.PrimaryProductImage != null)
        {
            var uploadFileCommand = new UploadPrimaryProductImageCommandRequest(PathConstants.DefaultProductImagesPath, product.Id, request.PrimaryProductImage);
            try
            {
                await _mediator.Send(uploadFileCommand, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new FailedDependencyException("Error while uploading primary image.", innerException: ex);
            }
        }

        if (request.SecondaryProductImages != null && request.SecondaryProductImages.Count > 0)
        {
            var uploadFilesCommand = new UploadSecondaryProductImagesCommandRequest(PathConstants.DefaultProductImagesPath, product.Id, request.SecondaryProductImages);

            try
            {
                await _mediator.Send(uploadFilesCommand, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new FailedDependencyException("Error while uploading secondary images.", innerException: ex);
            }
        }

        var addedProduct = await _productWriteRepository.AddAsync(product);
        var detailedProduct = await _productReadRepository.GetByIdAsync(addedProduct.Id, include: [product => product.Categories, product => product.ProductImageFiles])
            ?? throw new NotFoundException(nameof(Product), addedProduct.Id);

        var productDto = _mapper.Map<ProductDto>(detailedProduct);

        response.SetData(productDto);
        await _productHubService.SendProductAddedAsync(productDto);

        return response;
    }
}