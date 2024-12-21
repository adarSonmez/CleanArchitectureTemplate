namespace CommercePortal.Domain.Entities.Identity;

/**
 * About Domain Entities:
 *
 * Domain entities are core building blocks of the domain layer, representing key objects and their behaviors within the business logic.
 * They are designed to be independent of any external systems or infrastructure, ensuring that the domain layer remains isolated and adheres
 * to clean architecture principles.
 *
 * Key Characteristics of Domain Entities:
 *
 * - **Independence from External Systems:** Domain entities are decoupled from specific implementations, such as identity systems, to
 *   enhance testability and reduce dependencies.
 * - **Behavior Over Data:** Domain entities encapsulate business rules and behaviors, ensuring that they remain consistent and valid.
 * - **Non-Inheritance of EntityBase:** Unlike other entities, domain entities like `DomainUser` are not inherited from a base entity
 *   (e.g., `EntityBase`). This is intentional to keep the domain layer independent of shared infrastructure.
 *
 * About `DomainUser`:
 *
 * The `DomainUser` entity represents a user in the domain layer and is independent of any specific identity system. It acts as an abstraction
 * that isolates business logic from external identity providers or frameworks (e.g., ASP.NET Core Identity). This allows the domain layer to
 * remain agnostic to changes in the underlying identity system while still supporting core functionalities like user management.
 *
 * Dependent Entities:
 *
 * - Entities like `DomainUser` may have corresponding representations in other layers, such as persistence entities or DTOs.
 * - These dependent entities are mapped to the domain entities (e.g., using an AutoMapper profile) and reside in the Persistence project
 *   or Application layer as appropriate.
 */

/// <summary>
/// Represents a user within the domain layer, independent of any external identity systems.
/// </summary>
public class DomainUser
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
    public string? FullName { get; set; }

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
    public string PasswordHash { get; set; }

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