using CommercePortal.Application.Repositories;
using CommercePortal.Application.RequestParameters;
using CommercePortal.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
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
    private readonly IQueryable<TEntity> _qTable;

    public EfReadRepository(DbContext context)
    {
        _context = context;
        ListSource = _context.Set<TEntity>();
        _qTable = (ListSource as DbSet<TEntity>)!;
    }

    /// <inheritdoc/>
    public IListSource ListSource { get; set; }

    /// <inheritdoc/>
    public async Task<IEnumerable<TEntity>> GetAllPaginatedAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        IEnumerable<Expression<Func<TEntity, object>>>? include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool enableTracking = false, bool getDeleted = false, Pagination? pagination = null)
    {
        var table = _qTable;

        if (_qTable == null)
            return [];

        if (!enableTracking)
            table = table.AsNoTrackingWithIdentityResolution();

        if (predicate != null) table = table.Where(predicate);

        if (include != null)
            foreach (var inEntity in include)
                table = table.Include(inEntity);

        if (orderBy != null) table = orderBy(table);

        if (!getDeleted && typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
            table = table.Where(e => !(e as BaseEntity)!.IsDeleted);

        if (pagination == null)
            return await table.ToListAsync();

        return await table.Skip((pagination.Page - 1) * pagination.Size).Take(pagination.Size).ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        IEnumerable<Expression<Func<TEntity, object>>>? include = null,
        bool enableTracking = false,
        bool getDeleted = false)
    {
        var table = _qTable;

        if (!enableTracking)
            table = table.AsNoTracking();

        if (include != null)
            foreach (var inEntity in include)
                table = table.Include(inEntity);

        if (!getDeleted && typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
            table = table.Where(e => !(e as BaseEntity)!.IsDeleted);

        return await table.FirstOrDefaultAsync(predicate);
    }

    /// <inheritdoc/>
    public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
        bool enableTracking = false)
    {
        var table = _qTable;

        if (!enableTracking)
            table = table.AsNoTrackingWithIdentityResolution();

        return table.Where(predicate);
    }

    /// <inheritdoc/>
    public async Task<long> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        var table = _qTable;

        if (predicate != null)
            table = table.Where(predicate);

        return await table.LongCountAsync();
    }

    /// <inheritdoc/>
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}