namespace CommercePortal.Application.Features.Commands.Products.CreateProduct;

/// <summary>
/// Represents the response of the <see cref="CreateProductCommandRequest"/>.
/// </summary>
/// <param name="Id">The identifier of the created product.</param></param>
public record CreateProductCommandResponse(Guid Id);