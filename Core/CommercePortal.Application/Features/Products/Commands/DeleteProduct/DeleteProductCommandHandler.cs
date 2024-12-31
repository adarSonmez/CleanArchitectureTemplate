using CommercePortal.Application.Abstractions.Repositories.Marketing;
using CommercePortal.Application.Common.Responses;
using MediatR;

namespace CommercePortal.Application.Features.Products.Commands.DeleteProduct;

/// <summary>
/// Handles the <see cref="DeleteProductCommandRequest"/>.
/// </summary>
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, SingleResponse<Guid>>
{
    private readonly IProductWriteRepository _productWriteRepository;

    public DeleteProductCommandHandler(IProductWriteRepository productWriteRepository)
    {
        _productWriteRepository = productWriteRepository;
    }

    public async Task<SingleResponse<Guid>> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<Guid>();
        try
        {
            await _productWriteRepository.SoftDeleteAsync(request.Id);
            response.SetData(request.Id);
        }
        catch (Exception ex)
        {
            response.AddError("PRD677818", ex.Message);
        }
        return response;
    }
}