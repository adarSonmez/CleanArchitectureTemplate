using CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.DeleteProductImagesByProductId;
using CleanArchitectureTemplate.Application.Mappings.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Products.Commands.DeleteProduct;

/// <summary>
/// Handles the <see cref="DeleteProductCommandRequest"/>.
/// </summary>
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, SingleResponse<ProductDto?>>
{
    private readonly IMediator _mediator;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IUserContextService _userContextService;

    public DeleteProductCommandHandler(IMediator mediator, IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IUserContextService userContextService)
    {
        _mediator = mediator;
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _userContextService = userContextService;
    }

    public async Task<SingleResponse<ProductDto?>> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<ProductDto?>();

        var product = await _productReadRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Product), request.Id);

        if (!_userContextService.IsAdminOrSelf(product.StoreId))
            throw new ForbiddenException();

        var deleteProductImagesCommand = new DeleteProductImagesByProductIdCommandRequest(product!.Id);
        await _mediator.Send(deleteProductImagesCommand, cancellationToken);
        await _productWriteRepository.SoftDeleteAsync(product!);

        response.SetData(product.ToDto());

        return response;
    }
}