using CommercePortal.Domain.Entities.Common;

namespace CommercePortal.Application.Repositories
{
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
        /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Adds a range of entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of added entities.</returns>
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Updates an existing entity synchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        T Update(T entity);

        /// <summary>
        /// Performs a hard delete of an entity synchronously.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void HardDelete(T entity);

        /// <summary>
        /// Performs a hard delete of an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task HardDeleteAsync(string id);

        /// <summary>
        /// Performs a hard delete of entities matching the provided IDs asynchronously.
        /// </summary>
        /// <param name="ids">The IDs of the entities to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task HardDeleteMatchingAsync(IEnumerable<string> ids);

        /// <summary>
        /// Performs a hard delete of entities matching the provided entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        void HardDeleteMatching(params T[] entities);

        /// <summary>
        /// Performs a hard delete of entities matching the provided entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        void HardDeleteMatching(IEnumerable<T> entities);

        /// <summary>
        /// Performs a soft delete of an entity synchronously.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void SoftDelete(T entity);

        /// <summary>
        /// Performs a soft delete of an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SoftDeleteAsync(string id);

        /// <summary>
        /// Performs a soft delete of entities matching the provided IDs asynchronously.
        /// </summary>
        /// <param name="ids">The IDs of the entities to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SoftDeleteMatchingAsync(IEnumerable<string> ids);

        /// <summary>
        /// Performs a soft delete of entities matching the provided entities ssynchronously.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        void SoftDeleteMatching(params T[] entities);

        /// <summary>
        /// Performs a soft delete of entities matching the provided entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        void SoftDeleteMatching(IEnumerable<T> entities);

        /// <summary>
        /// Saves all changes made in the context to the database asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync();
    }
}