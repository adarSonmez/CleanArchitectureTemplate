using CommercePortal.Application.Common.Responses;
using MediatR;

namespace CommercePortal.Application.Features.ProductImageFiles.Commands.DeleteProductImageById;

/// <summary>
/// Represents the request for deleting a product image by identifier.
/// </summary>
/// <param name="ProductId">The identifier of the product whose image is to be deleted.</param>
public record class DeleteProductImageByIdRequest(Guid ProductId) : IRequest<SingleResponse<bool>>;