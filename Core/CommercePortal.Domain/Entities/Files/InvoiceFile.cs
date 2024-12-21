using CommercePortal.Domain.Common;
using CommercePortal.Domain.Entities.Ordering;

namespace CommercePortal.Domain.Entities.Files;

/// <summary>
/// Represents an invoice file entity.
/// </summary>
public class InvoiceFile : BaseEntity
{
    /// <summary>
    /// Gets or sets the invoice.
    /// </summary>
    public Invoice Invoice { get; set; } = default!;

    /// <summary>
    /// Gets or sets the file details.
    /// </summary>
    public FileDetails FileDetails { get; set; } = default!;
}