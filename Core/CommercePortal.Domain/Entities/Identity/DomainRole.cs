namespace CommercePortal.Domain.Entities.Identity;

/**
 * About Domain Entities:
 *
 * Domain entities are the core objects within the domain layer that encapsulate business rules and behavior. They are designed to be
 * independent of infrastructure and external systems, such as identity providers, ensuring the domain layer adheres to clean architecture
 * principles and remains focused on business logic.
 *
 * Key Characteristics of Domain Entities:
 *
 * - **Independence from External Systems:** Domain entities are not tied to specific implementations, like identity frameworks, enabling
 *   flexibility and testability.
 * - **Behavior Over Data:** Domain entities focus on the rules and logic that define the behavior of your system.
 * - **No Inheritance from EntityBase:** Unlike other entities, `DomainRole` is not derived from `EntityBase` to maintain independence
 *   from infrastructure concerns.
 *
 * About `DomainRole`:
 *
 * The `DomainRole` entity represents a role within the domain layer that is independent of any specific identity system. It abstracts
 * the concept of roles, providing a clean interface for defining and managing user roles in the domain layer without relying on
 * implementation details of external identity frameworks (e.g., ASP.NET Core Identity).
 *
 * Usage:
 *
 * - The `DomainRole` is used in the domain layer to define roles that can be assigned to `DomainUser` or other entities.
 * - Roles within the domain are decoupled from persistence or identity-specific details. Any mapping to identity system roles (e.g.,
 *   database entities) occurs in the Persistence project or Application layer.
 *
 * Dependent Entities:
 *
 * - Each domain entity, including `DomainRole`, may have corresponding representations in other layers, such as persistence entities or DTOs.
 * - These dependent entities are created in the Persistence project and mapped back to the domain entity, ensuring the domain remains isolated.
 */

/// <summary>
/// Represents a role within the domain layer, independent of any external identity systems.
/// </summary>
public class DomainRole
{
    private Guid? _id;

    /// <summary>
    /// Gets or sets the unique identifier for the role.
    /// If not explicitly set, a new GUID is generated.
    /// </summary>
    public required Guid Id
    {
        get => _id ??= Guid.NewGuid();
        set => _id = value;
    }

    /// <summary>
    /// Gets or sets the name of the role.
    /// </summary>
    public required string Name { get; set; }
}