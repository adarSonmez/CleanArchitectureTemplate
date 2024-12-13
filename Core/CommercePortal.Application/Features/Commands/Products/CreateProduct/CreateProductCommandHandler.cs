using CommercePortal.Application.Repositories.Products;
using CommercePortal.Domain.Entities;
using MediatR;

namespace CommercePortal.Application.Features.Commands.Products.CreateProduct;

/// <summary>
/// Handles the <see cref="CreateProductCommandRequest"/>.
/// </summary>
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
{
    private readonly IProductWriteRepository _productWriteRepository;

    public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
    {
        _productWriteRepository = productWriteRepository;
    }

    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Stock = request.Stock ?? 0,
            Price = request.Price
        };
        await _productWriteRepository.AddAsync(product);
        return new CreateProductCommandResponse(product.Id);
    }
}