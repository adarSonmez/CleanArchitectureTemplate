using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Marketing;
using CleanArchitectureTemplate.Domain.Entities.Marketing;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Products.Commands.DeleteProduct;

/// <summary>
/// Represents the request for updating a <see cref="Product"/>.
/// </summary>
/// <param name="Id">The identifier of the product.</param>
public record DeleteProductCommandRequest(Guid Id) : IRequest<SingleResponse<ProductDto?>>;