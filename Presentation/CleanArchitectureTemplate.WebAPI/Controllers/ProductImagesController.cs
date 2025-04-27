using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.DeleteProductImageById;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.DeleteProductImagesByProductId;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.UploadPrimaryProductImage;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.UploadSecondaryProductImages;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Queries.GetProductImageById;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Queries.GetProductImagesByFolder;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Queries.GetProductImagesByProductId;
using CleanArchitectureTemplate.Application.RequestParameters;
using CleanArchitectureTemplate.Domain.Constants.StringConstants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
public class ProductImagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductImagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var request = new GetProductImageByIdQueryRequest(id);
        return await _mediator.Send(request);
    }

    [HttpGet("product/{productId}")]
    [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false, VaryByQueryKeys = ["*"])]
    public async Task<IActionResult> GetByProductId([FromQuery] Pagination pagination, [FromRoute] Guid productId)
    {
        var request = new GetProductImagesByProductIdQueryRequest(pagination, productId);
        return await _mediator.Send(request);
    }

    [HttpGet("folder/{folderName}")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> GetByFolderName([FromQuery] Pagination pagination, [FromRoute] string folderName)
    {
        var request = new GetProductImagesByFolderQueryRequest(pagination, folderName);
        return await _mediator.Send(request);
    }

    [HttpPost("upload-primary")]
    [Authorize(Roles = UserRoles.StoreOrAdmin)]
    public async Task<IActionResult> UploadPrimaryImage([FromForm] UploadPrimaryProductImageCommandRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost("upload-secondary/{productId}")]
    [Authorize(Roles = UserRoles.StoreOrAdmin)]
    public async Task<IActionResult> UploadSecondaryImages(
        [FromBody] string folder,
        [FromRoute] Guid productId,
        [FromForm] IFormFileCollection files)
    {
        var request = new UploadSecondaryProductImagesCommandRequest(folder, productId, files);
        return await _mediator.Send(request);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = UserRoles.StoreOrAdmin)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var request = new DeleteProductImageByIdCommandRequest(id);
        return await _mediator.Send(request);
    }

    [HttpDelete("product/{productId}")]
    [Authorize(Roles = UserRoles.StoreOrAdmin)]
    public async Task<IActionResult> DeleteByProductId([FromRoute] Guid productId)
    {
        var request = new DeleteProductImagesByProductIdCommandRequest(productId);
        return await _mediator.Send(request);
    }
}