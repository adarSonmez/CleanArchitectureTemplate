namespace CommercePortal.Domain.Constants.Enums;

/// <summary>
/// Represents the status of an order.
/// </summary>
public enum OrderStatus
{
    Pending,
    Paid,
    Shipped,
    Delivered,
    Canceled
}