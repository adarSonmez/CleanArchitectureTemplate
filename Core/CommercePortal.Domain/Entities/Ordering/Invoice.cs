using CommercePortal.Domain.Common;
using CommercePortal.Domain.Constants.Enums;
using CommercePortal.Domain.ValueObjects;

namespace CommercePortal.Domain.Entities.Ordering;

/// <summary>
/// Represents an invoice for an order.
/// </summary>
public class Invoice : BaseEntity
{
    /// <summary>
    /// The associated order for the invoice.
    /// </summary>
    public Order Order { get; set; } = default!;

    /// <summary>
    /// The billing address for the invoice.
    /// </summary>
    public Address BillingAddress { get; set; } = default!;

    /// <summary>
    /// The issue date of the invoice.
    /// </summary>
    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The due date of the invoice.
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// The total amount for the invoice.
    /// </summary>
    public Money? TotalAmount => CalculateTotalAmount();

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

    #region Private Methods

    /// <summary>
    /// Computes the total amount of the invoice based on the order. Apllies any discounts or taxes.
    /// </summary>
    private Money? CalculateTotalAmount()
    {
        if (Order == null || Order.OrderItems.Count == 0)
        {
            return null;
        }

        var total = Order.OrderItems.Sum(item => item.TotalPrice.Amount);
        return new Money(total, Order.OrderItems.First().TotalPrice.Currency);
    }

    #endregion Private Methods
}