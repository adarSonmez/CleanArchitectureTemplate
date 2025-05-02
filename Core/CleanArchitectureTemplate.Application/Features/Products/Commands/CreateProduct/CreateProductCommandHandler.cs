using CleanArchitectureTemplate.Application.Abstractions.Hubs;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.UploadPrimaryProductImage;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.UploadSecondaryProductImages;
using CleanArchitectureTemplate.Application.Constants.StringContants;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Application.Exceptions;
using MediatR;
using CleanArchitectureTemplate.Application.Mappings.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Files;

namespace CleanArchitectureTemplate.Application.Features.Products.Commands.CreateProduct;

/// <summary>
/// Handles the <see cref="CreateProductCommandRequest"/>.
/// </summary>
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, SingleResponse<ProductDto?>>
{
    private readonly IMediator _mediator;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly ICategoryReadRepository _categoryReadRepository;
    private readonly IProductHubService _productHubService;
    private readonly IUserContextService _userContextService;

    public CreateProductCommandHandler(
        IMediator mediator,
        IProductReadRepository productReadRepository,
        IProductWriteRepository productWriteRepository,
        ICategoryReadRepository categoryReadRepository,
        IProductHubService productHubService,
        IUserContextService userContextService)
    {
        _mediator = mediator;
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _categoryReadRepository = categoryReadRepository;
        _productHubService = productHubService;
        _userContextService = userContextService;
    }

    public async Task<SingleResponse<ProductDto?>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<ProductDto?>();

        var userId = _userContextService.GetUserId()!;

        var product = request.ToEntity();
        var categories = await _categoryReadRepository.GetByIdRangeAsync(request.CategoryIds, enableTracking: true);

        product.Categories = [.. categories];
        product.StoreId = userId.Value;
        var addedProduct = await _productWriteRepository.AddAsync(product);

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

        var includes = new List<string>
        {
            nameof(Product.Categories),
            nameof(Product.ProductImageFiles),
            $"{nameof(Product.ProductImageFiles)}.{nameof(ProductImageFile.FileDetails)}"
        };

        var detailedProduct = await _productReadRepository.GetByIdAsync(addedProduct.Id, includePaths: includes)
            ?? throw new NotFoundException(nameof(Product), addedProduct.Id);

        var productDto = detailedProduct.ToDto();

        response.SetData(productDto);

        await _productHubService.SendProductAddedAsync(productDto);
        await _categoryReadRepository.DisableTrackingAsync(categories);

        return response;
    }
}