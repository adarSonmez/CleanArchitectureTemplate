using CleanArchitectureTemplate.Domain.Common;
using CleanArchitectureTemplate.Domain.Constants.Enums;
using CleanArchitectureTemplate.Domain.Exceptions;
using CleanArchitectureTemplate.Domain.ValueObjects;

namespace CleanArchitectureTemplate.Domain.Entities.Ordering;

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
    /// The foreign key for the order.
    /// </summary>
    public Guid OrderId { get; set; }

    /// <summary>
    /// The order associated with the invoice.
    /// </summary>
    public Order Order { get; set; } = default!;

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
            throw new ForbiddenException("Only pending invoices can be marked as paid.");
        }

        if (!IsOverdue())
        {
            throw new ForbiddenException("Only overdue invoices can be marked as paid.");
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