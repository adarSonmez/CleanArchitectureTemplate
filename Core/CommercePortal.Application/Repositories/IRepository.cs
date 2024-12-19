using CommercePortal.Domain.Common;
using System.ComponentModel;

namespace CommercePortal.Application.Repositories;

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