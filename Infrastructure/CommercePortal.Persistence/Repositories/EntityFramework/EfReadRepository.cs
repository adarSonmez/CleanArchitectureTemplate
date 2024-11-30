using CommercePortal.Application.Repositories;
using CommercePortal.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CommercePortal.Persistence.Repositories.EntityFramework;

/// <summary>
/// Represents the Entity Framework implementation of the IEntityRepository interface.
/// </summary>
/// <typeparam name="TEntity">The type of entity.</typeparam>
/// <typeparam name="TContext">The type of DbContext.</typeparam>
public class EfReadRepository<TEntity> : IReadRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbContext _context;

    public EfReadRepository(DbContext context)
    {
        _context = context;
        Table = _context.Set<TEntity>();
    }

    /// <inheritdoc/>
    public DbSet<TEntity> Table { get; set; }

    /// <inheritdoc/>
    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool enableTracking = false,
        bool getDeleted = false)
    {
        var table = Table.AsQueryable();

        if (!enableTracking)
            table = table.AsNoTrackingWithIdentityResolution();

        if (predicate != null) table = table.Where(predicate);

        if (include != null) table = include(table);

        if (orderBy != null) table = orderBy(table);

        if (!getDeleted && typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
            table = table.Where(e => !(e as BaseEntity)!.IsDeleted);

        return await table.ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TEntity>> GetAllPaginatedAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool enableTracking = false, bool getDeleted = false, int currentPage = 1, int pageSize = int.MaxValue)
    {
        var table = Table.AsQueryable();

        if (!enableTracking)
            table = table.AsNoTrackingWithIdentityResolution();

        if (predicate != null) table = table.Where(predicate);

        if (include != null) table = include(table);

        if (orderBy != null) table = orderBy(table);

        if (!getDeleted && typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
            table = table.Where(e => !(e as BaseEntity)!.IsDeleted);

        return await table.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null, bool enableTracking = false,
        bool getDeleted = false)
    {
        var table = Table.AsQueryable();

        if (!enableTracking)
            table = table.AsNoTracking();

        if (include != null)
            table = include(table);

        if (!getDeleted && typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
            table = table.Where(e => !(e as BaseEntity)!.IsDeleted);

        return await table.FirstOrDefaultAsync(predicate);
    }

    /// <inheritdoc/>
    public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
        bool enableTracking = false)
    {
        var table = Table.AsQueryable();

        if (!enableTracking)
            table = table.AsNoTrackingWithIdentityResolution();

        return table.Where(predicate);
    }

    /// <inheritdoc/>
    public async Task<long> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        var table = Table.AsQueryable();

        if (predicate != null)
            table = table.Where(predicate);

        return await table.LongCountAsync();
    }
}