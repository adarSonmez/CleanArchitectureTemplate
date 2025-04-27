using CleanArchitectureTemplate.Application.RequestParameters;
using CleanArchitectureTemplate.Domain.Shared;
using System.Linq.Expressions;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories;

/// <summary>
/// Represents a read-only repository interface for accessing entities of type <typeparamref name="TEntity"/>.
/// Provides methods for retrieving, finding, and counting entities asynchronously with support for filtering,
/// including related entities, ordering, pagination, and tracking options.
/// </summary>
/// <typeparam name="TEntity">The type of the entity inherited from <see cref="BaseEntity"/>.</typeparam>
public interface IReadRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Retrieves all entities asynchronously with pagination.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities.</param>
    /// <param name="include">The list of functions to include related entities.</param>
    /// <param name="orderBy">The function to order entities.</param>
    /// <param name="enableTracking">Indicates whether to enable entity tracking.</param>
    /// <param name="getDeleted">Indicates whether to include deleted entities.</param>
    /// <param name="pagination">Optional pagination parameters.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of entities and the total count.</returns>
    Task<(IEnumerable<TEntity> Data, int TotalCount)> GetAllPaginatedAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        IEnumerable<Expression<Func<TEntity, object>>>? include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool enableTracking = false, bool getDeleted = false, Pagination? pagination = null);

    /// <summary>
    /// Retrieves a single entity asynchronously based on the predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter the entity.</param>
    /// <param name="include">The list of functions to include related entities.</param>
    /// <param name="enableTracking">Indicates whether to enable entity tracking.</param>
    /// <param name="getDeleted">Indicates whether to include deleted entities.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity.</returns>
    Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        IEnumerable<Expression<Func<TEntity, object>>>? include = null,
        bool enableTracking = false,
        bool getDeleted = false);

    /// <summary>
    /// Retrieves a single entity asynchronously based on the ID.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <param name="include">The list of functions to include related entities.</param>
    /// <param name="enableTracking">Indicates whether to enable entity tracking.</param>
    /// <param name="getDeleted">Indicates whether to include deleted entities.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity.</returns>
    /// <remarks>Use this method to retrieve a single entity by its unique identifier.</remarks>
    /// <exception cref="KeyNotFoundException">Thrown when the entity with the specified ID is not found.</exception>
    Task<TEntity?> GetByIdAsync(
    Guid id,
    IEnumerable<Expression<Func<TEntity, object>>>? include = null,
    bool enableTracking = false,
    bool getDeleted = false);

    /// <summary>
    /// Retrieves all entities asynchronously with provided IDs.
    /// </summary>
    /// <param name="ids">The list of entity IDs to retrieve.</param>
    /// <param name="include">The list of functions to include related entities.</param>
    /// <param name="orderBy">The function to order entities.</param>
    /// <param name="enableTracking">Indicates whether to enable entity tracking.</param>
    /// <param name="getDeleted">Indicates whether to include deleted entities.</param>
    /// <param name="pagination">Optional pagination parameters.</param>
    /// <param name="throwIfNotFound">Indicates whether to throw an exception if an entity is not found.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of entities.</returns>
    Task<IEnumerable<TEntity>> GetByIdRangeAsync(
        IEnumerable<Guid> ids,
        IEnumerable<Expression<Func<TEntity, object>>>? include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool enableTracking = false, bool getDeleted = false, Pagination? pagination = null,
        bool throwIfNotFound = true);

    /// <summary>
    /// Finds entities synchronously based on the predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities.</param>
    /// <param name="enableTracking">Indicates whether to enable entity tracking.</param>
    /// <returns>The queryable collection of entities.</returns>
    /// <remarks>The query is not executed until the collection is enumerated.</remarks>
    IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool enableTracking = false);

    /// <summary>
    /// Counts the number of entities asynchronously based on the predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the count of entities.</returns>
    Task<long> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);

    /// <summary>
    /// Saves all changes made in the context to the database asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
    /// <remarks>Make sure enable-tracking is set to true before calling this method.</remarks>
    Task<int> SaveChangesAsync();
}