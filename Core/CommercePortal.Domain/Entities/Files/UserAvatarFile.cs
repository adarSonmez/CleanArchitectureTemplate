using CommercePortal.Domain.Common;
using CommercePortal.Domain.Entities.Identity;

namespace CommercePortal.Domain.Entities.Files;

/// <summary>
/// Represents a user avatar file entity.
/// </summary>
public class UserAvatarFile : BaseEntity
{
    /// <summary>
    /// Gets or sets the user associated with the avatar file.
    /// </summary>
    public DomainUser DomainUser { get; set; } = default!;

    /// <summary>
    /// Gets or sets the id of the user associated with the avatar file.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the file details.
    /// </summary>
    public FileDetails FileDetails { get; set; } = default!;
}