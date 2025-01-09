namespace CleanArchitectureTemplate.Domain.Constants.Enums;

/// <summary>
/// Represents the status of an invoice.
/// </summary>
public enum InvoiceStatus
{
    Pending,
    Paid,
    Overdue,
    Canceled
}