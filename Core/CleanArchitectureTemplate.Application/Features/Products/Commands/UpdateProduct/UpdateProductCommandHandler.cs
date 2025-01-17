﻿using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Marketing;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Marketing;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.DeleteProductImagesByProductId;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.UploadPrimaryProductImage;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.UploadSecondaryProductImages;
using CleanArchitectureTemplate.Domain.Common;
using CleanArchitectureTemplate.Domain.Constants.StringContants;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Products.Commands.UpdateProduct;

/// <summary>
/// Handles the <see cref="UpdateProductCommandRequest"/>.
/// </summary>
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, SingleResponse<ProductDto?>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly ICategoryReadRepository _categoryReadRepository;

    public UpdateProductCommandHandler(IMediator mediator, IMapper mapper, IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, ICategoryReadRepository categoryReadRepository)
    {
        _mediator = mediator;
        _mapper = mapper;
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _categoryReadRepository = categoryReadRepository;
    }

    public async Task<SingleResponse<ProductDto?>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<ProductDto?>();
        try
        {
            var product = await _productReadRepository.GetByIdAsync(request.Id);
            BusinessRules.Run(("PRD233224", BusinessRules.CheckEntityNull(product)));

            if (request.CategoryIds?.Count > 0)
            {
                var categories = await _categoryReadRepository.GetByIdRangeAsync(request.CategoryIds);
                product!.Categories = categories.ToList();
            }

            if (request.PrimaryProductImage != null)
            {
                var uploadFileCommand = new UploadPrimaryProductImageCommandRequest(PathConstants.DefaultProductImagesPath, product!.Id, request.PrimaryProductImage);
                var uploadFileResponse = await _mediator.Send(uploadFileCommand, cancellationToken);
                if (!uploadFileResponse.IsSuccessful)
                {
                    response.AddError("PRD309234", uploadFileResponse.Messages.FirstOrDefault()?.Message ?? "Error while uploading primary image.");
                    return response;
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
                    var uploadFilesResponse = await _mediator.Send(uploadFilesCommand, cancellationToken);
                    if (!uploadFilesResponse.IsSuccessful)
                    {
                        response.AddError("PRD427158", uploadFilesResponse.Messages.FirstOrDefault()?.Message ?? "Error while uploading secondary images.");
                        return response;
                    }
                }
            }

            var uodatedProduct = await _productWriteRepository.UpdateAsync(product!);
            var detailedProduct = await _productReadRepository.GetByIdAsync(uodatedProduct.Id, include: [product => product.Categories, product => product.ProductImageFiles]);

            response.SetData(_mapper.Map<ProductDto>(detailedProduct));
        }
        catch (Exception ex)
        {
            response.AddError("PRD329495", ex.Message);
        }

        return response;
    }
}