using CommercePortal.Application.Abstractions.Storage;
using CommercePortal.Application.Repositories.Files;
using CommercePortal.Application.Repositories.Products;
using CommercePortal.Domain.Entities;
using MediatR;

namespace CommercePortal.Application.Features.Commands.ProductImageFiles.UploadProductImage;

/// <summary>
/// Handles the <see cref="UploadProductImageCommandRequest"/>
/// </summary>
public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
{
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IStorageService _storageService;

    public UploadProductImageCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository, IStorageService storageService, IProductReadRepository productReadRepository)
    {
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _storageService = storageService;
        _productReadRepository = productReadRepository;
    }

    public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.GetAsync(e => e.Id == request.ProductId, enableTracking: true);

        if (product == null)
        {
            throw new Exception($"Product with id {request.ProductId} not found.");
        }

        (string folder, string fileName) = await _storageService.UploadFileAsync(request.Folder, request.File);

        var productImage = await _productImageFileWriteRepository.AddAsync(new ProductImageFile
        {
            Name = fileName,
            Folder = folder,
            StorageName = _storageService.StorageName
        });

        product.ProductImageFiles.Add(productImage);
        await _productReadRepository.SaveChangesAsync();

        return new UploadProductImageCommandResponse(Id: productImage.Id, Folder: folder, File: fileName);
    }
}