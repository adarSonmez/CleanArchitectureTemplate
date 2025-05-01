using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.DeleteProductImagesByProductId;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.UploadPrimaryProductImage;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.UploadSecondaryProductImages;
using CleanArchitectureTemplate.Application.Constants.StringContants;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using MediatR;
using CleanArchitectureTemplate.Application.Mappings.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Files;

namespace CleanArchitectureTemplate.Application.Features.Products.Commands.UpdateProduct;

/// <summary>
/// Handles the <see cref="UpdateProductCommandRequest"/>.
/// </summary>
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, SingleResponse<ProductDto?>>
{
    private readonly IMediator _mediator;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly ICategoryReadRepository _categoryReadRepository;
    private readonly IUserContextService _userContextService;

    public UpdateProductCommandHandler(IMediator mediator, IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, ICategoryReadRepository categoryReadRepository, IUserContextService userContextService)
    {
        _mediator = mediator;
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _categoryReadRepository = categoryReadRepository;
        _userContextService = userContextService;
    }

    public async Task<SingleResponse<ProductDto?>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<ProductDto?>();

        var product = await _productReadRepository.GetByIdAsync(request.Id)
        ?? throw new NotFoundException(nameof(Product), request.Id);

        if (!_userContextService.IsAdminOrSelf(product.StoreId))
            throw new ForbiddenException();

        if (request.CategoryIds?.Count > 0)
        {
            var categories = await _categoryReadRepository.GetByIdRangeAsync(request.CategoryIds);
            product!.Categories = categories.ToList();
        }

        if (request.PrimaryProductImage != null)
        {
            var uploadFileCommand = new UploadPrimaryProductImageCommandRequest(PathConstants.DefaultProductImagesPath, product!.Id, request.PrimaryProductImage);
            try
            {
                await _mediator.Send(uploadFileCommand, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new FailedDependencyException("Error while uploading primary image.", innerException: ex);
            }
        }

        if (request.SecondaryProductImages != null)
        {
            if (request.DeleteExistingSecondaryImages || request.SecondaryProductImages.Count == 0)
            {
                var deleteProductImagesCommand = new DeleteProductImagesByProductIdCommandRequest(product!.Id);
                await _mediator.Send(deleteProductImagesCommand, cancellationToken);
            }

            if (request.SecondaryProductImages.Count > 0)
            {
                var uploadFilesCommand = new UploadSecondaryProductImagesCommandRequest(PathConstants.DefaultProductImagesPath, product!.Id, request.SecondaryProductImages);
                try
                {
                    await _mediator.Send(uploadFilesCommand, cancellationToken);
                }
                catch (Exception ex)
                {
                    throw new FailedDependencyException("Error while uploading secondary images.", innerException: ex);
                }
            }
        }

        var includes = new List<string>
        {
            nameof(Product.Categories),
            nameof(Product.ProductImageFiles),
            $"{nameof(Product.ProductImageFiles)}.{nameof(ProductImageFile.FileDetails)}"
        };

        var uodatedProduct = await _productWriteRepository.UpdateAsync(product!);
        var detailedProduct = await _productReadRepository.GetByIdAsync(uodatedProduct.Id, includePaths: includes);

        response.SetData(detailedProduct?.ToDto());

        return response;
    }
}