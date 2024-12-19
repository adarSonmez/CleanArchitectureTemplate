namespace CommercePortal.Domain.Common;

/// <summary>
/// Represents a base entity which has common properties for all entities.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the deletation status of the entity.
    /// </summary>
    /// <remarks>Use this property to soft delete an entity. </remarks>
    public bool IsDeleted { get; set; }
}