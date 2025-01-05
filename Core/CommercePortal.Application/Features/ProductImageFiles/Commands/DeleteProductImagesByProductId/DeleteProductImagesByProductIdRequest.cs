using CommercePortal.Application.Common.Responses;
using CommercePortal.Domain.Entities.Files;
using MediatR;

namespace CommercePortal.Application.Features.ProductImageFiles.Commands.DeleteProductImagesByProductId;

/// <summary>
/// Represents the request for deleting a <see cref="ProductImageFile"/> by its ProductId.
/// </summary>
public record class DeleteProductImagesByProductIdRequest(Guid ProductId) : IRequest<SingleResponse<bool>>;