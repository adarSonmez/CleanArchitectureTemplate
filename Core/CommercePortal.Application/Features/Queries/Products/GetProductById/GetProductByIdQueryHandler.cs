using CommercePortal.Application.Repositories.Products;
using MediatR;

namespace CommercePortal.Application.Features.Queries.Products.GetProductById;

/// <summary>
/// Handles the <see cref="GetProductByIdQueryRequest"/>.
/// </summary>
public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, GetProductByIdQueryResponse>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetProductByIdQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.GetAsync(e => e.Id == request.Id);

        return product == null
            ? throw new Exception($"Product with id {request.Id} not found.")
            : new
            (
                Id: product.Id,
                Name: product.Name,
                Description: product.Description ?? string.Empty,
                Price: product.Price,
                Stock: product.Stock
            );
    }
}