namespace CommercePortal.Application.Features.Commands.Products.DeleteProduct;

/// <summary>
/// Represents the response of the <see cref="DeleteProductCommandRequest"/>.
/// </summary>
/// <param name="Id">The identifier of the deleted product.</param></param>
public record DeleteProductCommandResponse(Guid Id);