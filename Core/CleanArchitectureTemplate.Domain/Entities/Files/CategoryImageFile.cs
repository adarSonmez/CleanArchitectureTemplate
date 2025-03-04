﻿using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Domain.Shared;

namespace CleanArchitectureTemplate.Domain.Entities.Files;

/// <summary>
/// Represents a category image file entity.
/// </summary>
public class CategoryImageFile : BaseEntity
{
    /// <summary>
    /// Gets or sets the foreign key for the FileDetails.
    /// </summary>
    public Guid FileDetailsId { get; set; }

    /// <summary>
    /// Gets or sets the file details.
    /// </summary>
    public FileDetails? FileDetails { get; set; }

    /// <summary>
    /// Gets or sets the foreign key for the category.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the category that the file belongs to.
    /// </summary>
    public Category? Category { get; set; }
}