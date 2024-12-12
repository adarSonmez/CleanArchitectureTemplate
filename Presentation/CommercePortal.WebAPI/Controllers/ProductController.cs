using CommercePortal.Application.Abstractions.Storage;
using CommercePortal.Application.Repositories.Files;
using CommercePortal.Application.Repositories.Products;
using CommercePortal.Application.RequestParameters;
using CommercePortal.Application.ViewModels.Products;
using CommercePortal.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CommercePortal.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IStorageService _storageService;

    public TestController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IStorageService storageService, IProductImageFileWriteRepository productImageFileWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _storageService = storageService;
        _productImageFileWriteRepository = productImageFileWriteRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] Pagination pagination)
    {
        var products = await _productReadRepository.GetAllPaginatedAsync(pagination: pagination);
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductVM product)
    {
        var productEntity = new Product
        {
            Name = product.Name,
            Description = product.Description,
            Stock = product.Stock ?? 0,
            Price = product.Price
        };
        await _productWriteRepository.AddAsync(productEntity);
        await _productWriteRepository.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        var targetPath = "images";
        (string folder, string fileName) = await _storageService.UploadFileAsync(targetPath, file);

        await _productImageFileWriteRepository.AddAsync(new ProductImageFile
        {
            Name = fileName,
            Folder = folder,
            StorageName = _storageService.StorageName
        });

        await _productImageFileWriteRepository.SaveChangesAsync();

        return Ok(new { Folder = folder, File = fileName });
    }
}