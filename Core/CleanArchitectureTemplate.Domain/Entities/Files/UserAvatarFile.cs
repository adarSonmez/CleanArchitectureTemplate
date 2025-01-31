using CleanArchitectureTemplate.Domain.Entities.Identity;
using CleanArchitectureTemplate.Domain.Shared;

namespace CleanArchitectureTemplate.Domain.Entities.Files;

/// <summary>
/// Represents a user avatar file entity.
/// </summary>
public class UserAvatarFile : BaseEntity
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
    /// Gets or sets the foreign key for the user.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the user that the file belongs to.
    /// </summary>
    public DomainUser DomainUser { get; set; } = default!;
}