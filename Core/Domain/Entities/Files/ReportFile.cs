using CleanArchitectureTemplate.Domain.Common;
using CleanArchitectureTemplate.Domain.Constants.Enums;

namespace CleanArchitectureTemplate.Domain.Entities.Files;

/// <summary>
/// Represents a report file entity.
/// </summary>
public class ReportFile : BaseEntity
{
    /// <summary>
    /// Gets or sets the report type associated with the file.
    /// </summary>
    public ReportType ReportType { get; set; }

    /// <summary>
    /// Gets or sets the foreign key for the FileDetails.
    /// </summary>
    public Guid FileDetailsId { get; set; }

    /// <summary>
    /// Gets or sets the file details.
    /// </summary>
    public FileDetails FileDetails { get; set; } = default!;
}