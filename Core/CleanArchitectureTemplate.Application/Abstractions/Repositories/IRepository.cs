using CleanArchitectureTemplate.Domain.Shared;
using System.ComponentModel;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories;

/// <summary>
/// Represents a common interface for read and write repositories.
/// </summary>
/// <typeparam name="TEntity">The type of entity inherited from <see cref="BaseEntity"/>.</typeparam>
public interface IRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Gets the list source of the entity type <typeparamref name="TEntity"/>.
    /// </summary>
    IListSource ListSource { get; set; }
}