using CommercePortal.Application.Abstractions.Repositories.Files;
using CommercePortal.Application.Abstractions.Services.Storage;
using CommercePortal.Application.Common.Responses;
using CommercePortal.Domain.Common;
using MediatR;

namespace CommercePortal.Application.Features.ProductImageFiles.Commands.DeleteProductImageById;

/// <summary>
/// Handles the <see cref="DeleteProductImageByIdRequest"/>
/// </summary>
public class DeleteProductImageByIdHandler : IRequestHandler<DeleteProductImageByIdRequest, SingleResponse<bool>>
{
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;
    private readonly IStorageService _storageService;

    public DeleteProductImageByIdHandler(IProductImageFileWriteRepository productImageFileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IStorageService storageService)
    {
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _productImageFileReadRepository = productImageFileReadRepository;
        _storageService = storageService;
    }

    public async Task<SingleResponse<bool>> Handle(DeleteProductImageByIdRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<bool>();
        try
        {
            var productImageFile = await _productImageFileReadRepository.GetByIdAsync(request.ProductId);
            BusinessRules.Run(("PIF640271", BusinessRules.CheckEntityNull(productImageFile)));

            await _productImageFileWriteRepository.HardDeleteAsync(productImageFile!.Id);
            await _storageService.DeleteFileAsync(productImageFile.FileDetails.Folder, productImageFile.FileDetails.Name);
            response.SetData(true);
        }
        catch (Exception ex)
        {
            response.AddError("PIF896067", ex.Message);
        }

        return response;
    }
}