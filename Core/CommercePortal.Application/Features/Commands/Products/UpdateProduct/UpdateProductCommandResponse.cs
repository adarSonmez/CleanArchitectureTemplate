namespace CommercePortal.Application.Features.Commands.Products.UpdateProduct;

/// <summary>
/// Represents the response of the <see cref="UpdateProductCommandRequest"/>.
/// </summary>
/// <param name="Id">The identifier of the updated product.</param></param>
public record UpdateProductCommandResponse(Guid Id);