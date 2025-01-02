using CommercePortal.Domain.Common;
using CommercePortal.Domain.Constants.Enums;
using CommercePortal.Domain.Entities.Files;
using CommercePortal.Domain.ValueObjects;

namespace CommercePortal.Domain.Entities.Ordering;

/// <summary>
/// Represents an invoice for an order.
/// </summary>
public class Invoice : BaseEntity
{
    /// <summary>
    /// The billing address for the invoice.
    /// </summary>
    public Address BillingAddress { get; set; } = default!;

    /// <summary>
    /// The foreign key for the invoice file.
    /// </summary>
    public Guid InvoiceFileId { get; set; }

    /// <summary>
    /// The invoice file which contains the invoice details.
    /// </summary>
    public InvoiceFile InvoiceFile { get; set; } = default!;

    /// <summary>
    /// The issue date of the invoice.
    /// </summary>
    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The due date of the invoice.
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// The status of the invoice (e.g., Pending, Paid, Overdue).
    /// </summary>
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;

    /// <summary>
    /// The payment method used for the invoice.
    /// </summary>
    public PaymentMethod? PaymentMethod { get; set; }

    /// <summary>
    /// The unique transaction reference for payment, if applicable.
    /// </summary>
    public string? TransactionId { get; set; }

    /// <summary>
    /// Any additional notes or comments related to the invoice.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// The date the invoice was paid, if applicable.
    /// </summary>
    public DateTime? PaidAt { get; set; }

    #region Public Methods

    /// <summary>
    /// Mark the invoice as paid.
    /// </summary>
    public void MarkAsPaid(PaymentMethod paymentMethod, string transactionId)
    {
        if (Status != InvoiceStatus.Pending)
        {
            throw new InvalidOperationException("Only pending invoices can be marked as paid.");
        }

        PaymentMethod = paymentMethod;
        TransactionId = transactionId;
        Status = InvoiceStatus.Paid;
        PaidAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Check if the invoice is overdue.
    /// </summary>
    /// <returns>True if the invoice is overdue, otherwise false.</returns>
    public bool IsOverdue()
    {
        return Status == InvoiceStatus.Pending && DueDate < DateTime.UtcNow;
    }

    #endregion Public Methods
}