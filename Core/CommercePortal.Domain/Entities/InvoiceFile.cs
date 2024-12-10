namespace CommercePortal.Domain.Entities;

/// <summary>
/// Represents an invoice file entity.
/// </summary>
public class InvoiceFile : File
{
    /// <summary>
    /// Gets or sets the price of the invoice.
    /// </summary>
    public decimal Price { get; set; }
}