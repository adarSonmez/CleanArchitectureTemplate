using CommercePortal.Application.Repositories.Products;
using CommercePortal.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CommercePortal.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    public ProductController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }

    [HttpGet]
    public async Task Get()
    {
        // add some dummy data

        var entities = await _productWriteRepository.AddRangeAsync(
        [
            new Product { Name = "Product 1", Price = 100 },
            new Product { Name = "Product 2", Price = 200 },
            new Product { Name = "Product 3", Price = 300 }
        ]);

        await _productWriteRepository.SaveChangesAsync();
    }
}