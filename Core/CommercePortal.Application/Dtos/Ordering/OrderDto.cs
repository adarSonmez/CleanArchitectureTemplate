using CommercePortal.Application.Dtos.Membership;
using CommercePortal.Domain.Constants.Enums;
using CommercePortal.Domain.MarkerInterfaces;
using CommercePortal.Domain.ValueObjects;

namespace CommercePortal.Application.Dtos.Ordering;

/// <summary>
/// Represents the invoice data transfer object.
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="Status">The status of the order.</param>
/// <param name="TotalAmount">The total amount of the order.</param>
/// <param name="ShippingAddress">The shipping address of the order.</param>
/// <param name="Customer">The customer data transfer object.</param>
/// <param name="Invoice">The invoice data transfer object.</param>
/// <param name="OrderItems">The order items of the order.</param>
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