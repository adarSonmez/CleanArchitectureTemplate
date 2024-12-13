using CommercePortal.Application.Repositories.Products;
using MediatR;

namespace CommercePortal.Application.Features.Commands.Products.DeleteProduct;

/// <summary>
/// Handles the <see cref="DeleteProductCommandRequest"/>.
/// </summary>
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
{
    private readonly IProductWriteRepository _productWriteRepository;

    public DeleteProductCommandHandler(IProductWriteRepository productWriteRepository)
    {
        _productWriteRepository = productWriteRepository;
    }

    public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        await _productWriteRepository.SoftDeleteAsync(request.Id);

        return new DeleteProductCommandResponse(request.Id);
    }
}