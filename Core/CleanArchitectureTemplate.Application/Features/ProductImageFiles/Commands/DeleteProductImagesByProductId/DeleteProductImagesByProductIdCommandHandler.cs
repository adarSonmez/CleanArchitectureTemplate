using CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Abstractions.Services.Storage;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.DeleteProductImagesByProductId;

/// <summary>
/// Handles the <see cref="DeleteProductImagesByProductIdCommandRequest"/>
/// </summary>
public class DeleteProductImagesByProductIdCommandHandler : IRequestHandler<DeleteProductImagesByProductIdCommandRequest, ResponseResult>
{
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IStorageService _storageService;
    private readonly IUserContextService _userContextService;

    public DeleteProductImagesByProductIdCommandHandler(
        IProductImageFileWriteRepository productImageFileWriteRepository,
        IProductImageFileReadRepository productImageFileReadRepository,
        IProductReadRepository productReadRepository,
        IStorageService storageService,
        IUserContextService userContextService)
    {
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _productImageFileReadRepository = productImageFileReadRepository;
        _productReadRepository = productReadRepository;
        _storageService = storageService;
        _userContextService = userContextService;
    }

    public async Task<ResponseResult> Handle(DeleteProductImagesByProductIdCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.GetByIdAsync(request.ProductId)
            ?? throw new NotFoundException(nameof(Product), request.ProductId);

        if (!_userContextService.IsAdminOrSelf(product.StoreId))
            throw new ForbiddenException();

        var includes = new List<string>
        {
            nameof(ProductImageFile.FileDetails)
        };

        var (productImageFiles, _) = await _productImageFileReadRepository.GetAllPaginatedAsync(x => x.ProductId == request.ProductId, includePaths: includes);

        if (productImageFiles == null || !productImageFiles.Any()) throw new NotFoundException($"No product images found for the product with ID: {request.ProductId}");

        foreach (var productImageFile in productImageFiles)
        {
            await _productImageFileWriteRepository.HardDeleteAsync(productImageFile.Id, saveChanges: false);
            await _storageService.DeleteFileAsync(productImageFile.FileDetails!.Folder, productImageFile.FileDetails.Name);
        }

        await _productImageFileWriteRepository.SaveChangesAsync();
        return new();
    }
}