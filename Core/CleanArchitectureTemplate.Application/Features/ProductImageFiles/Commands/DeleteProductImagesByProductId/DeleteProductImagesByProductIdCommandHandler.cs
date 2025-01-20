using CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;
using CleanArchitectureTemplate.Application.Abstractions.Services.Storage;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Domain.Exceptions;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.DeleteProductImagesByProductId;

/// <summary>
/// Handles the <see cref="DeleteProductImagesByProductIdCommandRequest"/>
/// </summary>
public class DeleteProductImagesByProductIdCommandHandler : IRequestHandler<DeleteProductImagesByProductIdCommandRequest, SingleResponse<bool>>
{
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;
    private readonly IStorageService _storageService;

    public DeleteProductImagesByProductIdCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IStorageService storageService)
    {
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _productImageFileReadRepository = productImageFileReadRepository;
        _storageService = storageService;
    }

    public async Task<SingleResponse<bool>> Handle(DeleteProductImagesByProductIdCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<bool>();

        var productImageFiles = await _productImageFileReadRepository.GetAllPaginatedAsync(x => x.Product.Id == request.ProductId)
            ?? throw new NotFoundException($"No product images found for the product with ID: {request.ProductId}");

        foreach (var productImageFile in productImageFiles)
        {
            await _productImageFileWriteRepository.HardDeleteAsync(productImageFile.Id, saveChanges: false);
            await _storageService.DeleteFileAsync(productImageFile.FileDetails.Folder, productImageFile.FileDetails.Name);
        }

        await _productImageFileWriteRepository.SaveChangesAsync();
        response.SetData(true);

        return response;
    }
}