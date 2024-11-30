using CommercePortal.Application.Repositories;
using CommercePortal.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace CommercePortal.Persistence.Repositories.EntityFramework;

/// <summary>
/// Represents the Entity Framework implementation of the IEntityRepository interface.
/// </summary>
/// <typeparam name="TEntity">The type of entity.</typeparam>
/// <typeparam name="TContext">The type of DbContext.</typeparam>
public class EfWriteRepository<TEntity> : IWriteRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbContext _context;

    public EfWriteRepository(DbContext context)
    {
        _context = context;
        Table = _context.Set<TEntity>();
    }

    /// <inheritdoc/>
    public DbSet<TEntity> Table { get; set; }

    #region Create Methods

    /// <inheritdoc/>
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _context.AddAsync(entity);
        return entity;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _context.AddRangeAsync(entities);
        return entities;
    }

    #endregion Create Methods

    #region Update Methods

    /// <inheritdoc/>
    public TEntity Update(TEntity entity)
    {
        _context.Update(entity);
        return entity;
    }

    #endregion Update Methods

    #region Hard Delete Methods

    /// <inheritdoc/>
    public void HardDelete(TEntity entity)
    {
        _context.Remove(entity);
    }

    /// <inheritdoc/>
    public async Task HardDeleteAsync(string id)
    {
        var entity = await Table.FindAsync(id);
        if (entity != null)
            _context.Remove(entity);
    }

    /// <inheritdoc/>
    public async Task HardDeleteMatchingAsync(IEnumerable<string> ids)
    {
        foreach (var id in ids)
        {
            var entity = await Table.FindAsync(id);
            if (entity != null)
                _context.Remove(entity);
        }
    }

    /// <inheritdoc/>
    public void HardDeleteMatching(params TEntity[] entities)
    {
        _context.RemoveRange(entities.ToList());
    }

    /// <inheritdoc/>
    public void HardDeleteMatching(IEnumerable<TEntity> entities)
    {
        _context.RemoveRange(entities);
    }

    #endregion Hard Delete Methods

    #region Soft Delete Methods

    /// <inheritdoc/>
    public void SoftDelete(TEntity entity)
    {
        (entity as BaseEntity)!.IsDeleted = true;
        _context.Update(entity);
    }

    /// <inheritdoc/>
    public async Task SoftDeleteAsync(string id)
    {
        var entity = await Table.FindAsync(id);
        if (entity == null)
            return;

        (entity as BaseEntity)!.IsDeleted = true;
        _context.Update(entity);
    }

    /// <inheritdoc/>
    public async Task SoftDeleteMatchingAsync(IEnumerable<string> ids)
    {
        foreach (var id in ids)
        {
            var entity = await Table.FindAsync(id);
            if (entity == null)
                continue;

            (entity as BaseEntity)!.IsDeleted = true;
            _context.Update(entity);
        }
    }

    /// <inheritdoc/>
    public void SoftDeleteMatching(params TEntity[] entities)
    {
        foreach (var entity in entities)
            (entity as BaseEntity)!.IsDeleted = true;
        _context.UpdateRange(entities.ToList());
    }

    /// <inheritdoc/>
    public void SoftDeleteMatching(IEnumerable<TEntity> entities)
    {
        var enumerable = entities as TEntity[] ?? entities.ToArray();
        foreach (var entity in enumerable)
            (entity as BaseEntity)!.IsDeleted = true;
        _context.UpdateRange(enumerable.ToList());
    }

    #endregion Soft Delete Methods

    #region Save Changes

    /// <inheritdoc/>
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    #endregion Save Changes
}