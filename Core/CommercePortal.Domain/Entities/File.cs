using CommercePortal.Domain.Entities.Common;

namespace CommercePortal.Domain.Entities;

/// <summary>
/// Represents a file entity. (e.g. image, document, etc.)
/// </summary>
public class File : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the file.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the file extension.
    /// </summary>
    public string? Extension { get; set; }

    /// <summary>
    /// Gets or sets the file size in bytes.
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// Gets or sets the file path.
    /// </summary>
    public required string Path { get; set; }
}