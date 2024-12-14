using CommercePortal.Application.Repositories.Products;
using MediatR;

namespace CommercePortal.Application.Features.Commands.Products.UpdateProduct;

/// <summary>
/// Handles the <see cref="UpdateProductCommandRequest"/>.
/// </summary>
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
{
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductReadRepository _productReadRepository;

    public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.GetAsync(e => e.Id == request.Id);

        if (product == null)
        {
            throw new Exception($"Product with id {request.Id} not found.");
        }

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            product.Name = request.Name;
        }

        if (!string.IsNullOrWhiteSpace(request.Description))
        {
            product.Description = request.Description;
        }

        if (request.Stock.HasValue)
        {
            product.Stock = request.Stock.Value;
        }

        if (request.Price.HasValue)
        {
            product.Price = request.Price.Value;
        }

        await _productWriteRepository.UpdateAsync(product);

        return new UpdateProductCommandResponse(product.Id);
    }
}