using CleanArchitectureTemplate.Domain.Common;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories;

/// <summary>
/// Represents a write-only repository interface for managing entities of type <typeparamref name="T"/>.
/// Provides methods for adding, updating, and deleting entities asynchronously with support for both hard and soft delete operations.
/// </summary>
/// <typeparam name="TEntity">The type of the entity inherited from <see cref="BaseEntity"/>.</typeparam>
public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Adds a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="saveChanges">A value indicating whether to save changes to the database after adding the entity.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
    Task<T> AddAsync(T entity, bool saveChanges = true);

    /// <summary>
    /// Adds a range of entities asynchronously.
    /// </summary>
    /// <param name="entities">The entities to add.</param>
    /// <param name="saveChanges">A value indicating whether to save changes to the database after adding the entities.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of added entities.</returns>
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, bool saveChanges = true);

    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="saveChanges">A value indicating whether to save changes to the database after updating the entity.</param>
    /// <returns>The updated entity.</returns>
    Task<T> UpdateAsync(T entity, bool saveChanges = true);

    /// <summary>
    /// Performs a hard delete of an entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="saveChanges">A value indicating whether to save changes to the database after deleting the entity.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task HardDeleteAsync(T entity, bool saveChanges = true);

    /// <summary>
    /// Performs a hard delete of an entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <param name="saveChanges">A value indicating whether to save changes to the database after deleting the entity.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task HardDeleteAsync(Guid id, bool saveChanges = true);

    /// <summary>
    /// Performs a hard delete of entities matching the provided IDs asynchronously.
    /// </summary>
    /// <param name="ids">The IDs of the entities to delete.</param>
    /// <param name="saveChanges">A value indicating whether to save changes to the database after deleting the entities.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task HardDeleteMatchingAsync(IEnumerable<Guid> ids, bool saveChanges = true);

    /// <summary>
    /// Performs a hard delete of entities matching the provided entities asynchronously.
    /// </summary>
    /// <param name="saveChanges">A value indicating whether to save changes to the database after deleting the entities.</param>
    /// <param name="entities">The entities to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task HardDeleteMatchingAsync(bool saveChanges = true, params T[] entities);

    /// <summary>
    /// Performs a hard delete of entities matching the provided entities asynchronously.
    /// </summary>
    /// <param name="entities">The entities to delete.</param>
    /// <param name="saveChanges">A value indicating whether to save changes to the database after deleting the entities.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task HardDeleteMatchingAsync(IEnumerable<T> entities, bool saveChanges = true);

    /// <summary>
    /// Performs a soft delete of an entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="saveChanges">A value indicating whether to save changes to the database after deleting the entity.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SoftDeleteAsync(T entity, bool saveChanges = true);

    /// <summary>
    /// Performs a soft delete of an entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <param name="saveChanges">A value indicating whether to save changes to the database after deleting the entity.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SoftDeleteAsync(Guid id, bool saveChanges = true);

    /// <summary>
    /// Performs a soft delete of entities matching the provided IDs asynchronously.
    /// </summary>
    /// <param name="ids">The IDs of the entities to delete.</param>
    /// <param name="saveChanges">A value indicating whether to save changes to the database after deleting the entities.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SoftDeleteMatchingAsync(IEnumerable<Guid> ids, bool saveChanges = true);

    /// <summary>
    /// Performs a soft delete of entities matching the provided entities asynchronously.
    /// </summary>
    /// <param name="saveChanges">A value indicating whether to save changes to the database after deleting the entities.</param>
    /// <param name="entities">The entities to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SoftDeleteMatchingAsync(bool saveChanges = true, params T[] entities);

    /// <summary>
    /// Performs a soft delete of entities matching the provided entities synchronously.
    /// </summary>
    /// <param name="entities">The entities to delete.</param>
    /// <param name="saveChanges">A value indicating whether to save changes to the database after deleting the entities.</param>
    Task SoftDeleteMatchingAsync(IEnumerable<T> entities, bool saveChanges = true);

    /// <summary>
    /// Saves all changes made in the context to the database asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync();
}