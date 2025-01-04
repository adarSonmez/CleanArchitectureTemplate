using CommercePortal.Domain.Entities.Ordering;
using CommercePortal.Domain.MarkerInterfaces;
using CommercePortal.Domain.ValueObjects;

namespace CommercePortal.Application.Dtos.Ordering;

/// <summary>
/// Represents data transfer object for <see cref="OrderItem"/>
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="ProductId">The identifier of the product.</param>
/// <param name="Quantity">The quantity of the product.</param>
/// <param name="TotalPrice">The total price of the order item.</param>
public record OrderItemDto
(
    Guid Id,
    Guid ProductId,
    int Quantity,
    Money TotalPrice
) : IDto;