using AutoMapper;
using CommercePortal.Application.Abstractions.Repositories.Files;
using CommercePortal.Application.Abstractions.Repositories.Marketing;
using CommercePortal.Application.Abstractions.Services.Storage;
using CommercePortal.Application.Common.Responses;
using CommercePortal.Application.Dtos.Files;
using CommercePortal.Domain.Common;
using CommercePortal.Domain.Constants.SmartEnums.Files;
using CommercePortal.Domain.Entities.Files;
using MediatR;

namespace CommercePortal.Application.Features.ProductImageFiles.Commands.UploadPrimaryProductImage;

/// <summary>
/// Handles the <see cref="UploadPrimaryProductImageCommandRequest"/>
/// </summary>
public class UploadPrimaryProductImageCommandHandler : IRequestHandler<UploadPrimaryProductImageCommandRequest, SingleResponse<ProductImageFileDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IStorageService _storageService;

    public UploadPrimaryProductImageCommandHandler(IMapper mapper, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IProductReadRepository productReadRepository, IStorageService storageService)
    {
        _mapper = mapper;
        _productImageFileReadRepository = productImageFileReadRepository;
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _productReadRepository = productReadRepository;
        _storageService = storageService;
    }

    public async Task<SingleResponse<ProductImageFileDto>> Handle(UploadPrimaryProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<ProductImageFileDto>();

        try
        {
            var product = await _productReadRepository.GetByIdAsync(request.ProductId);
            BusinessRules.Run(("PIF250880", BusinessRules.CheckEntityNull(product)));

            var existingPrimaryImage = await _productImageFileReadRepository.GetAsync(x => x.Product.Id == request.ProductId && x.IsPrimary);

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
                Product = product!
            };

            await _productImageFileWriteRepository.AddAsync(productImageFile);
            response.SetData(_mapper.Map<ProductImageFileDto>(productImageFile));
        }
        catch (Exception ex)
        {
            response.AddError("PIF282355", ex.Message);
        }

        return response;
    }
}