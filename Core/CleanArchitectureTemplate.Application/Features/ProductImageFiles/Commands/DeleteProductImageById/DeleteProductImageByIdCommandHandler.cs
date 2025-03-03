using CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Abstractions.Services.Storage;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using MediatR;
using System.Linq.Expressions;

namespace CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.DeleteProductImageById;

/// <summary>
/// Handles the <see cref="DeleteProductImageByIdCommandRequest"/>
/// </summary>
public class DeleteProductImageByIdCommandHandler : IRequestHandler<DeleteProductImageByIdCommandRequest, ResponseResult>
{
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IStorageService _storageService;
    private readonly IUserContextService _userContextService;

    public DeleteProductImageByIdCommandHandler(
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

    public async Task<ResponseResult> Handle(DeleteProductImageByIdCommandRequest request, CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<ProductImageFile, object>>>
        {
            pi => pi.Product!,
            pi => pi.FileDetails!
        };

        var productImageFile = await _productImageFileReadRepository.GetByIdAsync(request.ProductId, include: includes)
            ?? throw new NotFoundException(nameof(ProductImageFile), request.ProductId);

        var product = await _productReadRepository.GetByIdAsync(productImageFile.Product!.Id)
            ?? throw new NotFoundException(nameof(Product), productImageFile.Product.Id);

        if (!_userContextService.IsAdminOrSelf(product.StoreId))
            throw new ForbiddenException();

        await _productImageFileWriteRepository.HardDeleteAsync(productImageFile.Id);
        await _storageService.DeleteFileAsync(productImageFile.FileDetails!.Folder, productImageFile.FileDetails.Name);

        return new();
    }
}