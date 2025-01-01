namespace CommercePortal.Application.Dtos.Ordering;

using CommercePortal.Application.Dtos.Files;
using CommercePortal.Domain.Constants.Enums;
using CommercePortal.Domain.ValueObjects;

/// <summary>
/// Represents the invoice data transfer object.
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="BillingAddress">The billing address of the invoice.</param>
/// <param name="InvoiceFile">The invoice file data transfer object.</param>
/// <param name="IssuedAt">The date and time when the invoice was issued.</param>
/// <param name="DueDate">The due date of the invoice.</param>
/// <param name="Status">The status of the invoice.</param>
/// <param name="PaymentMethod">The payment method of the invoice.</param>
/// <param name="TransactionId">The transaction id of the invoice.</param>
/// <param name="Notes">The notes of the invoice.</param>
/// <param name="PaidAt">The date and time when the invoice was paid.</param>
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