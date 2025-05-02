using CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Abstractions.Services.Storage;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Files;
using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Application.Mappings.Files;
using CleanArchitectureTemplate.Domain.Constants.SmartEnums.Files;
using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.UploadPrimaryProductImage;

/// <summary>
/// Handles the <see cref="UploadPrimaryProductImageCommandRequest"/>
/// </summary>
public class UploadPrimaryProductImageCommandHandler : IRequestHandler<UploadPrimaryProductImageCommandRequest, SingleResponse<ProductImageFileDto>>
{
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IStorageService _storageService;
    private readonly IUserContextService _userContextService;

    public UploadPrimaryProductImageCommandHandler(
        IProductImageFileReadRepository productImageFileReadRepository,
        IProductImageFileWriteRepository productImageFileWriteRepository,
        IProductReadRepository productReadRepository,
        IStorageService storageService,
        IUserContextService userContextService)
    {
        _productImageFileReadRepository = productImageFileReadRepository;
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _productReadRepository = productReadRepository;
        _storageService = storageService;
        _userContextService = userContextService;
    }

    public async Task<SingleResponse<ProductImageFileDto>> Handle(UploadPrimaryProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<ProductImageFileDto>();

        var product = await _productReadRepository.GetByIdAsync(request.ProductId)
            ?? throw new NotFoundException(nameof(Product), request.ProductId);

        if (!_userContextService.IsAdminOrSelf(product.StoreId))
            throw new ForbiddenException();

        var existingPrimaryImage = await _productImageFileReadRepository.GetAsync(x => x.ProductId == request.ProductId && x.IsPrimary);

        if (existingPrimaryImage != null)
        {
            existingPrimaryImage.IsPrimary = false;
            await _productImageFileWriteRepository.UpdateAsync(existingPrimaryImage);
        }

        (string folder, string name, long size) = await _storageService.UploadFileAsync(request.Folder, request.File);

        var fileExtension = Path.GetExtension(name);
        var fileDetails = new FileDetails
        {
            Folder = folder,
            Name = name,
            Storage = _storageService.StorageType,
            Extension = FileExtension.FromExtension(fileExtension),
            Size = size
        };

        var productImageFile = new ProductImageFile
        {
            FileDetails = fileDetails,
            IsPrimary = true,
            ProductId = product.Id
        };

        await _productImageFileWriteRepository.AddAsync(productImageFile);
        response.SetData(productImageFile.ToDto());

        return response;
    }
}