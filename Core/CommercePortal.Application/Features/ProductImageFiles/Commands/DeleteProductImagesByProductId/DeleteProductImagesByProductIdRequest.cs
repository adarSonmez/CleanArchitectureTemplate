using CommercePortal.Application.Common.Responses;
using MediatR;

namespace CommercePortal.Application.Features.ProductImageFiles.Commands.DeleteProductImagesByProductId;

/// <summary>
/// Represents the request for deleting product images by product identifier.
/// </summary>
public record class DeleteProductImagesByProductIdRequest(Guid ProductId) : IRequest<SingleResponse<bool>>;