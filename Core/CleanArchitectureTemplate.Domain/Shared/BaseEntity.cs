﻿using CleanArchitectureTemplate.Domain.MarkerInterfaces;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitectureTemplate.Domain.Shared;

/// <summary>
/// Represents a base entity which has common properties for all entities.
/// </summary>
/// <remarks>
/// Other audit properties like CreatedBy, UpdatedBy and DeletedBy are added as Shadow Properties.<br/>
/// So they are not added to this class.
/// </remarks>
public abstract class BaseEntity : IEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the deletation status of the entity.
    /// </summary>
    /// <remarks>Use this property to soft delete an entity. </remarks>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the creation time of the entity.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last modification time of the entity.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the deletion time of the entity.
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Gets or sets the row version for concurrency checks.
    /// </summary>
    /// <remarks>
    /// For SQL Server: RowVersion system → byte[]
    /// </remarks>
    [Timestamp]
    public byte[]? RowVersion { get; set; }

    /// <summary>
    /// Gets or sets the version for concurrency checks.
    /// </summary>
    /// <remarks>
    /// For PostgreSQL: xmin system → uint[]
    /// </remarks>
    [Timestamp]
    public uint? Version { get; set; }
}