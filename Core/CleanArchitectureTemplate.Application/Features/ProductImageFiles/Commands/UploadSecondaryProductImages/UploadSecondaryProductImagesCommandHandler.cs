using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Abstractions.Services.Storage;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Files;
using CleanArchitectureTemplate.Domain.Constants.SmartEnums.Files;
using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Application.Exceptions;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.UploadSecondaryProductImages;

/// <summary>
/// Handles the <see cref="UploadSecondaryProductImagesCommandRequest"/>
/// </summary>
public class UploadSecondaryProductImagesCommandHandler : IRequestHandler<UploadSecondaryProductImagesCommandRequest, PagedResponse<ProductImageFileDto?>>
{
    private readonly IMapper _mapper;
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IStorageService _storageService;
    private readonly IUserContextService _userContextService;

    public UploadSecondaryProductImagesCommandHandler(
        IMapper mapper,
        IProductImageFileWriteRepository productImageFileWriteRepository,
        IProductReadRepository productReadRepository,
        IStorageService storageService,
        IUserContextService userContextService)
    {
        _mapper = mapper;
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _productReadRepository = productReadRepository;
        _storageService = storageService;
        _userContextService = userContextService;
    }

    public async Task<PagedResponse<ProductImageFileDto?>> Handle(UploadSecondaryProductImagesCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new PagedResponse<ProductImageFileDto?>();

        var product = await _productReadRepository.GetByIdAsync(request.ProductId)
            ?? throw new NotFoundException(nameof(Product), request.ProductId);

        if (!_userContextService.IsAdminOrSelf(product.StoreId))
            throw new ForbiddenException();

        var productImageFiles = new List<ProductImageFile>();
        var uploadResult = await _storageService.UploadFilesAsync(request.Folder, request.Files);

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
                IsPrimary = false,
                Product = product!
            };

            productImageFiles.Add(productImageFile);
        }

        await _productImageFileWriteRepository.AddRangeAsync(productImageFiles);
        response.SetData(_mapper.Map<List<ProductImageFileDto>>(productImageFiles), request.Pagination?.Page, request.Pagination?.Size);

        return response;
    }
}