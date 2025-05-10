using CleanArchitectureTemplate.Application.Abstractions.Repositories;
using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Application.RequestParameters;
using CleanArchitectureTemplate.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq.Expressions;

namespace CleanArchitectureTemplate.Persistence.Repositories.EntityFramework;

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
    public async Task<(IEnumerable<TEntity> Data, int TotalCount)> GetAllPaginatedAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        IEnumerable<string>? includePaths = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool enableTracking = false,
        bool getDeleted = false,
        Pagination? pagination = null,
        bool throwIfNotFound = false)
    {
        var table = _qTable;

        if (table == null)
            return ([], 0);

        if (!enableTracking)
            table = table.AsNoTrackingWithIdentityResolution();

        if (predicate != null)
            table = table.Where(predicate);

        if (includePaths != null)
        {
            foreach (var includePath in includePaths)
            {
                table = table.Include(includePath);
            }
        }

        if (orderBy != null)
            table = orderBy(table);

        if (!getDeleted && typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
            table = table.Where(e => !(e as BaseEntity)!.IsDeleted);

        var totalCount = await table.CountAsync();

        if (pagination != null)
        {
            table = table.Skip((pagination.Page - 1) * pagination.Size)
                         .Take(pagination.Size);
        }

        var data = await table.ToListAsync();

        if (data == null || data.Count == 0)
        {
            if (throwIfNotFound)
                throw new NotFoundException($"No entities found for {typeof(TEntity).Name} with the specified predicate.");
            return ([], 0);
        }

        return (data, totalCount);
    }

    /// <inheritdoc/>
    public async Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        IEnumerable<string>? includePaths = null,
        bool enableTracking = false,
        bool getDeleted = false,
        bool throwIfNotFound = false)
    {
        var table = _qTable;

        if (!enableTracking)
            table = table.AsNoTracking();

        if (includePaths != null)
        {
            foreach (var includePath in includePaths)
            {
                table = table.Include(includePath);
            }
        }

        if (!getDeleted && typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
            table = table.Where(e => !(e as BaseEntity)!.IsDeleted);

        var entity = await table.FirstOrDefaultAsync(predicate);

        if (entity == null && throwIfNotFound)
            throw new NotFoundException($"The entity {typeof(TEntity).Name} was not found with the specified predicate.");

        return entity;
    }

    /// <inheritdoc/>
    public async Task<TEntity?> GetByIdAsync(
        Guid id,
        IEnumerable<string>? includePaths = null,
        bool enableTracking = false,
        bool getDeleted = false,
        bool throwIfNotFound = false)
    {
        var table = _qTable;

        if (!enableTracking)
            table = table.AsNoTrackingWithIdentityResolution();

        if (includePaths != null)
        {
            foreach (var includePath in includePaths)
            {
                table = table.Include(includePath);
            }
        }

        if (!getDeleted && typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
            table = table.Where(e => !(e as BaseEntity)!.IsDeleted);

        var entity = await table.FirstOrDefaultAsync(e => e.Id == id);

        if (entity == null && throwIfNotFound)
            throw new NotFoundException(typeof(TEntity).Name, id);

        return entity;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TEntity>> GetByIdRangeAsync(
        IEnumerable<Guid> ids,
        IEnumerable<string>? includePaths = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool enableTracking = false,
        bool getDeleted = false,
        Pagination? pagination = null,
        bool throwIfNotFound = false)
    {
        if (ids == null || !ids.Any())
        {
            if (throwIfNotFound)
                throw new ValidationFailedException($"The collection of IDs is null or empty for {typeof(TEntity).Name}");

            return [];
        }

        var table = _qTable;

        if (!enableTracking)
            table = table.AsNoTrackingWithIdentityResolution();

        if (includePaths != null)
        {
            foreach (var includePath in includePaths)
            {
                table = table.Include(includePath);
            }
        }

        if (!getDeleted && typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
            table = table.Where(e => !(e as BaseEntity)!.IsDeleted);

        table = table.Where(e => ids.Contains(e.Id));
        if (orderBy != null)
            table = orderBy(table);

        if (pagination != null)
            table = table.Skip((pagination.Page - 1) * pagination.Size).Take(pagination.Size);

        var result = await table.ToListAsync();

        if (throwIfNotFound && result.Count != ids.Count())
        {
            var missingIds = ids.Except(result.Select(e => e.Id)).ToList();
            throw new NotFoundException($"The following IDs were not found in {typeof(TEntity).Name}: {string.Join(", ", missingIds)}");
        }

        return result;
    }

    /// <inheritdoc/>
    public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool enableTracking = false)
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

    /// <inheritdoc/>
    public async Task DisableTrackingAsync(TEntity entity)
    {
        var entry = _context.Entry(entity);
        if (entry.State == EntityState.Detached)
            return;
        entry.State = EntityState.Detached;
        await Task.CompletedTask;
    }

    /// <inheritdoc/>
    public async Task DisableTrackingAsync(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            await DisableTrackingAsync(entity);
        }
    }
}