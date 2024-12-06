using CommercePortal.Application.Repositories.Products;
using CommercePortal.Application.RequestParameters;
using CommercePortal.Application.ViewModels.Products;
using CommercePortal.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CommercePortal.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public ProductController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment hostingEnvironment)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _hostingEnvironment = hostingEnvironment;
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
        if (file is null)
        {
            return BadRequest();
        }
        var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }
        var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
        return Ok(new { filePath });
    }
}