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
    public DomainUser User { get; set; } = default!;

    /// <summary>
    /// Gets or sets the file details.
    /// </summary>
    public FileDetails FileDetails { get; set; } = default!;
}