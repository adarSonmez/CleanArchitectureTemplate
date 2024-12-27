namespace CommercePortal.Application.Dtos.Ordering;

using CommercePortal.Application.Dtos.Files;
using CommercePortal.Domain.Constants.Enums;
using CommercePortal.Domain.ValueObjects;

/// <summary>
/// Represents the invoice data transfer object.
/// </summary>
public record InvoiceDto
(
    Guid Id,
    Address BillingAddress,
    InvoiceFileDto? InvoiceFile,
    DateTime IssuedAt,
    DateTime DueDate,
    InvoiceStatus Status,
    PaymentMethod PaymentMethod,
    string? TransactionId,
    string? Notes,
    DateTime? PaidAt
);