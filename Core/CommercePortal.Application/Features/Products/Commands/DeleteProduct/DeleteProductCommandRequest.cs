using CommercePortal.Application.Common.Responses;
using CommercePortal.Application.Dtos.Marketing;
using CommercePortal.Domain.Entities.Marketing;
using MediatR;

namespace CommercePortal.Application.Features.Products.Commands.DeleteProduct;

/// <summary>
/// Represents the request for updating a <see cref="Product"/>.
/// </summary>
/// <param name="Id">The identifier of the product.</param>
public record DeleteProductCommandRequest(Guid Id) : IRequest<SingleResponse<ProductDto>>;