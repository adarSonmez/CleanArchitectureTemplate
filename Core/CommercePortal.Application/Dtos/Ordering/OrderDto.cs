using CommercePortal.Application.Dtos.Membership;
using CommercePortal.Domain.Constants.Enums;
using CommercePortal.Domain.MarkerInterfaces;
using CommercePortal.Domain.ValueObjects;

namespace CommercePortal.Application.Dtos.Ordering;

/// <summary>
/// Represents the invoice data transfer object.
/// </summary>
public record OrderDto
(
    Guid Id,
    OrderStatus Status,
    Money? TotalAmount,
    Address ShippingAddress,
    CustomerDto? Customer,
    InvoiceDto? Invoice,
    ICollection<OrderItemDto>? OrderItems
) : IDto;