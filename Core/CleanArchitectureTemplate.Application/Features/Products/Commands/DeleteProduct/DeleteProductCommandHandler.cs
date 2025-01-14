using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Marketing;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Marketing;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.DeleteProductImagesByProductId;
using CleanArchitectureTemplate.Domain.Common;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Products.Commands.DeleteProduct;

/// <summary>
/// Handles the <see cref="DeleteProductCommandRequest"/>.
/// </summary>
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, SingleResponse<ProductDto>>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    public DeleteProductCommandHandler(IMapper mapper, IMediator mediator, IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IProductImageFileWriteRepository productImageFileWriteRepository)
    {
        _mapper = mapper;
        _mediator = mediator;
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }

    public async Task<SingleResponse<ProductDto>> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<ProductDto>();
        try
        {
            var product = await _productReadRepository.GetByIdAsync(request.Id);
            BusinessRules.Run(("PRD770975", BusinessRules.CheckEntityNull(product)));

            var deleteProductImagesCommand = new DeleteProductImagesByProductIdCommandRequest(product!.Id);
            await _mediator.Send(deleteProductImagesCommand, cancellationToken);
            await _productWriteRepository.SoftDeleteAsync(product!);

            response.SetData(_mapper.Map<ProductDto>(product));
        }
        catch (Exception ex)
        {
            response.AddError("PRD677818", ex.Message);
        }
        return response;
    }
}