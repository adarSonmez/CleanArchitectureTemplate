using CleanArchitectureTemplate.Domain.Entities.Ordering;
using CleanArchitectureTemplate.Domain.Shared;

namespace CleanArchitectureTemplate.Domain.Entities.Files;

/// <summary>
/// Represents an invoice file entity.
/// </summary>
public class InvoiceFile : BaseEntity
{
    /// <summary>
    /// Gets or sets the foreign key for the FileDetails.
    /// </summary>
    public Guid FileDetailsId { get; set; }

    /// <summary>
    /// Gets or sets the file details.
    /// </summary>
    public FileDetails FileDetails { get; set; } = default!;

    /// <summary>
    /// Gets or sets the foreign key for the invoice.
    /// </summary>
    public Guid InvoiceId { get; set; }

    /// <summary>
    /// Gets or sets the invoice that the file belongs to.
    /// </summary>
    public Invoice Invoice { get; set; } = default!;
}