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

namespace CommercePortal.Application.Features.ProductImageFiles.Commands.UploadProductImages;

/// <summary>
/// Handles the <see cref="UploadProductImagesCommandRequest"/>
/// </summary>
public class UploadProductImagesCommandHandler : IRequestHandler<UploadProductImagesCommandRequest, PagedResponse<ProductImageFileDto>>
{
    private readonly IMapper _mapper;
    private readonly IFileDetailsReadRepository _fileDetailsReadRepository;
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IStorageService _storageService;

    public UploadProductImagesCommandHandler(IMapper mapper, IFileDetailsReadRepository fileDetailsReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IProductReadRepository productReadRepository, IStorageService storageService)
    {
        _mapper = mapper;
        _fileDetailsReadRepository = fileDetailsReadRepository;
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _productReadRepository = productReadRepository;
        _storageService = storageService;
    }

    public async Task<PagedResponse<ProductImageFileDto>> Handle(UploadProductImagesCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new PagedResponse<ProductImageFileDto>();

        try
        {
            var product = await _productReadRepository.GetByIdAsync(request.ProductId);
            BusinessRules.Run(("PIF250880", BusinessRules.CheckEntityNull(product)));

            var productImageFiles = new List<ProductImageFile>();
            var uploadResult = await _storageService.UploadFilesAsync(request.Folder, request.Files);

            bool isFirstImage = true;

            foreach ((string folder, string name, long size) in uploadResult)
            {
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
                    IsPrimary = isFirstImage,
                    Product = product!
                };

                isFirstImage = false;
                productImageFiles.Add(productImageFile);
            }

            await _productImageFileWriteRepository.AddRangeAsync(productImageFiles);
            response.SetData(_mapper.Map<List<ProductImageFileDto>>(productImageFiles));
        }
        catch (Exception ex)
        {
            response.AddError("PIF282355", ex.Message);
        }

        return response;
    }
}