using CleanArchitectureTemplate.Domain.Constants.Enums;
using CleanArchitectureTemplate.Domain.Constants.SmartEnums.Files;
using CleanArchitectureTemplate.Domain.Shared;

namespace CleanArchitectureTemplate.Domain.Entities.Files;

/// <summary>
/// Represents the file details entity.
/// </summary>
/// <remarks>All file entities have composition relationship with this entity.</remarks>
public class FileDetails : BaseEntity
{
    #region Properties

    /// <summary>
    /// Gets or sets the file name with extension.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the file extension.
    /// </summary>
    public required FileExtension Extension { get; set; }

    /// <summary>
    /// Gets or sets the file size in bytes.
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// Gets the human-readable size of the file.
    /// </summary>
    public string HumanReadableSize => Size switch
    {
        < 1024 => $"{Size} B",
        < 1048576 => $"{Size / 1024.0:F2} KB",
        < 1073741824 => $"{Size / 1048576.0:F2} MB",
        _ => $"{Size / 1073741824.0:F2} GB"
    };

    /// <summary>
    /// Gets or sets the folder path of the file.
    /// </summary>
    /// <remarks>Empty string means the root folder.</remarks>
    public string Folder { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the storage type.
    /// </summary>
    public required StorageType Storage { get; set; }

    #endregion Properties
}