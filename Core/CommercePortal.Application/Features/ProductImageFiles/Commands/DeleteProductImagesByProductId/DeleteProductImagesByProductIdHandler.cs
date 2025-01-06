using CommercePortal.Application.Abstractions.Repositories.Files;
using CommercePortal.Application.Abstractions.Services.Storage;
using CommercePortal.Application.Common.Responses;
using MediatR;

namespace CommercePortal.Application.Features.ProductImageFiles.Commands.DeleteProductImagesByProductId;

/// <summary>
/// Handles the <see cref="DeleteProductImagesByProductIdRequest"/>
/// </summary>
public class DeleteProductImagesByProductIdHandler : IRequestHandler<DeleteProductImagesByProductIdRequest, SingleResponse<bool>>
{
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;
    private readonly IStorageService _storageService;

    public DeleteProductImagesByProductIdHandler(IProductImageFileWriteRepository productImageFileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IStorageService storageService)
    {
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _productImageFileReadRepository = productImageFileReadRepository;
        _storageService = storageService;
    }

    public async Task<SingleResponse<bool>> Handle(DeleteProductImagesByProductIdRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<bool>();
        try
        {
            var productImageFiles = await _productImageFileReadRepository.GetAllPaginatedAsync(x => x.Product.Id == request.ProductId);

            if (!productImageFiles.Any())
            {
                response.AddError("PIF250940", "No product images found for the product.");
                return response;
            }

            foreach (var productImageFile in productImageFiles)
            {
                await _productImageFileWriteRepository.HardDeleteAsync(productImageFile.Id, saveChanges: false);
                await _storageService.DeleteFileAsync(productImageFile.FileDetails.Folder, productImageFile.FileDetails.Name);
            }

            await _productImageFileWriteRepository.SaveChangesAsync();
            response.SetData(true);
        }
        catch (Exception ex)
        {
            response.AddError("PIF191120", ex.Message);
        }

        return response;
    }
}