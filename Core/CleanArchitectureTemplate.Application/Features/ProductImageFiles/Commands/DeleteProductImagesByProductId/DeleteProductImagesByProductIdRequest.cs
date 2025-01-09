using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Domain.Entities.Files;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.DeleteProductImagesByProductId;

/// <summary>
/// Represents the request for deleting a <see cref="ProductImageFile"/> by its ProductId.
/// </summary>
public record class DeleteProductImagesByProductIdRequest(Guid ProductId) : IRequest<SingleResponse<bool>>;