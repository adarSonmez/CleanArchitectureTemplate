namespace CommercePortal.Application.Dtos.Ordering;

using CommercePortal.Application.Dtos.Marketing;
using CommercePortal.Domain.MarkerInterfaces;
using CommercePortal.Domain.ValueObjects;

/// <summary>
/// Represents the order item data transfer object.
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="Product">The product data transfer object.</param>
/// <param name="Quantity">The quantity of the product.</param>
/// <param name="TotalPrice">The total price of the order item.</param>
public record OrderItemDto
(
    Guid Id,
    ProductDto? Product,
    int Quantity,
    Money TotalPrice
) : IDto;