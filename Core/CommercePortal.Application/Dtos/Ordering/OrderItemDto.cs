namespace CommercePortal.Application.Dtos.Ordering;

using CommercePortal.Application.Dtos.Marketing;
using CommercePortal.Domain.MarkerInterfaces;
using CommercePortal.Domain.ValueObjects;

/// <summary>
/// Represents the order item data transfer object.
/// </summary>
public record OrderItemDto
(
    Guid Id,
    ProductDto? Product,
    int Quantity,
    Money TotalPrice
) : IDto;