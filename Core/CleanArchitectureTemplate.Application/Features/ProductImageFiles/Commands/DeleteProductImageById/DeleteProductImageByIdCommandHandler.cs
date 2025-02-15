using CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;
using CleanArchitectureTemplate.Application.Abstractions.Services.Storage;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Application.Exceptions;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.DeleteProductImageById;

/// <summary>
/// Handles the <see cref="DeleteProductImageByIdCommandRequest"/>
/// </summary>
public class DeleteProductImageByIdCommandHandler : IRequestHandler<DeleteProductImageByIdCommandRequest, SingleResponse<bool>>
{
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;
    private readonly IStorageService _storageService;

    public DeleteProductImageByIdCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IStorageService storageService)
    {
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _productImageFileReadRepository = productImageFileReadRepository;
        _storageService = storageService;
    }

    public async Task<SingleResponse<bool>> Handle(DeleteProductImageByIdCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<bool>();

        var productImageFile = await _productImageFileReadRepository.GetByIdAsync(request.ProductId)
            ?? throw new NotFoundException(nameof(ProductImageFile), request.ProductId);

        await _productImageFileWriteRepository.HardDeleteAsync(productImageFile!.Id);
        await _storageService.DeleteFileAsync(productImageFile.FileDetails.Folder, productImageFile.FileDetails.Name);
        response.SetData(true);

        return response;
    }
}