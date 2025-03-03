using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Domain.Entities.Files;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.DeleteProductImageById;

/// <summary>
/// Represents the request for deleting a <see cref="ProductImageFile"/> by its ID."/>
/// </summary>
/// <param name="ProductId">The identifier of the product whose image is to be deleted.</param>
public record class DeleteProductImageByIdCommandRequest(Guid ProductId) : IRequest<ResponseResult>;