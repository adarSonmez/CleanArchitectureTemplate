using CommercePortal.Application.Common.Responses;
using CommercePortal.Domain.Entities.Files;
using MediatR;

namespace CommercePortal.Application.Features.ProductImageFiles.Commands.DeleteProductImageById;

/// <summary>
/// Represents the request for deleting a <see cref="ProductImageFile"/> by its ID."/>
/// </summary>
/// <param name="ProductId">The identifier of the product whose image is to be deleted.</param>
public record class DeleteProductImageByIdRequest(Guid ProductId) : IRequest<SingleResponse<bool>>;