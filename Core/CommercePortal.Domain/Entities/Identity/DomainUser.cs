using CommercePortal.Domain.MarkerInterfaces;

/**
 * Domain Entities Overview:
 *
 * Domain entities are core components representing business logic and behavior, isolated from external systems and infrastructure.
 *
 * Key Features:
 * - **Independence:** Decoupled from identity systems to enhance testability and reduce dependencies.
 * - **Behavior-Focused:** Encapsulate business rules to ensure consistency and validity.
 * - **No Base Inheritance:** Entities like `DomainUser` avoid inheritance from shared infrastructure classes.
 *
 * About `DomainUser`:
 * - Represents a user in the domain layer, independent of specific identity systems.
 * - Abstracts business logic from external identity frameworks, enabling adaptability and isolation.
 */

namespace CommercePortal.Domain.Entities.Identity;

/// <summary>
/// Represents a user within the domain layer, independent of any external identity systems.
/// </summary>
public class DomainUser : IEntity
{
    private Guid? _id;

    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// If not explicitly set, a new GUID is generated.
    /// </summary>
    public Guid Id
    {
        get => _id ??= Guid.NewGuid();
        set => _id = value;
    }

    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Gets or sets the username for this user.
    /// </summary>
    public required string UserName { get; set; }

    /// <summary>
    /// Gets or sets the email address for this user.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating whether the email address is confirmed.
    /// </summary>
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// Gets or sets the hashed password for this user.
    /// </summary>
    public string? PasswordHash { get; set; }

    /// <summary>
    /// Gets or sets the phone number associated with this user.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating whether the phone number is confirmed.
    /// </summary>
    public bool PhoneNumberConfirmed { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating whether two-factor authentication is enabled for this user.
    /// </summary>
    public bool TwoFactorEnabled { get; set; }

    /// <summary>
    /// Gets or sets the date and time (in UTC) when the user's lockout period ends.
    /// A value in the past indicates the user is not locked out.
    /// </summary>
    public DateTimeOffset? LockoutEnd { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating whether the user is eligible to be locked out.
    /// </summary>
    public virtual bool LockoutEnabled { get; set; }

    /// <summary>
    /// Gets or sets the number of failed login attempts for the current user.
    /// </summary>
    public virtual int AccessFailedCount { get; set; }
}