using CommercePortal.Application.RequestParameters;
using CommercePortal.Domain.Entities.Common;
using System.Linq.Expressions;

namespace CommercePortal.Application.Repositories;

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
    /// <param name="include">The function to include related entities.</param>
    /// <param name="orderBy">The function to order entities.</param>
    /// <param name="enableTracking">Indicates whether to enable entity tracking.</param>
    /// <param name="getDeleted">Indicates whether to include deleted entities.</param>
    /// <param name="pagination">Optional pagination parameters.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of entities.</returns>
    Task<IEnumerable<TEntity>> GetAllPaginatedAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool enableTracking = false, bool getDeleted = false, Pagination? pagination = null);

    /// <summary>
    /// Retrieves a single entity asynchronously based on the predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter the entity.</param>
    /// <param name="include">The function to include related entities.</param>
    /// <param name="enableTracking">Indicates whether to enable entity tracking.</param>
    /// <param name="getDeleted">Indicates whether to include deleted entities.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity.</returns>
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
        bool enableTracking = false, bool getDeleted = false);

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
}