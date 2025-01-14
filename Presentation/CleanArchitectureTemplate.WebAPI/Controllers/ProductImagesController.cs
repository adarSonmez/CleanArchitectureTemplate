using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.DeleteProductImageById;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.DeleteProductImagesByProductId;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.UploadPrimaryProductImage;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.UploadSecondaryProductImages;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Queries.GetProductImageById;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Queries.GetProductImagesByFolder;
using CleanArchitectureTemplate.Application.Features.ProductImageFiles.Queries.GetProductImagesByProductId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductImagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductImagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var request = new GetProductImageByIdQueryRequest(id);
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("product/{productId}")]
    public async Task<IActionResult> GetByProductId([FromRoute] Guid productId)
    {
        var request = new GetProductImageByProductIdQueryRequest(productId);
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("folder/{folderName}")]
    public async Task<IActionResult> GetByFolderName([FromRoute] string folderName)
    {
        var request = new GetProductImageByFolderQueryRequest(folderName);
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("upload-primary")]
    public async Task<IActionResult> UploadPrimaryImage([FromForm] UploadPrimaryProductImageCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("upload-secondaries")]
    public async Task<IActionResult> UploadSecondaryImages([FromForm] UploadSecondaryProductImagesCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var request = new DeleteProductImageByIdCommandRequest(id);
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete("product/{productId}")]
    public async Task<IActionResult> DeleteByProductId([FromRoute] Guid productId)
    {
        var request = new DeleteProductImagesByProductIdCommandRequest(productId);
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}