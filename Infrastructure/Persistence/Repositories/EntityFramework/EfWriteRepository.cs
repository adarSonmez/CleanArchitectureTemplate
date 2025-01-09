using CleanArchitectureTemplate.Application.Abstractions.Repositories;
using CleanArchitectureTemplate.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace CleanArchitectureTemplate.Persistence.Repositories.EntityFramework;

/// <summary>
/// Represents the Entity Framework implementation of the IEntityRepository interface.
/// </summary>
/// <typeparam name="TEntity">The type of entity.</typeparam>
/// <typeparam name="TContext">The type of DbContext.</typeparam>
public class EfWriteRepository<TEntity> : IWriteRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _table;

    public EfWriteRepository(DbContext context)
    {
        _context = context;
        ListSource = _context.Set<TEntity>();
        _table = (ListSource as DbSet<TEntity>)!;
    }

    /// <inheritdoc/>
    public IListSource ListSource { get; set; }

    #region Create Methods

    /// <inheritdoc/>
    public async Task<TEntity> AddAsync(TEntity entity, bool saveChanges = true)
    {
        await _context.AddAsync(entity);

        if (saveChanges)
            await SaveChangesAsync();

        return entity;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true)
    {
        await _context.AddRangeAsync(entities);

        if (saveChanges)
            await SaveChangesAsync();

        return entities;
    }

    #endregion Create Methods

    #region Update Methods

    /// <inheritdoc/>
    public async Task<TEntity> UpdateAsync(TEntity entity, bool saveChanges = true)
    {
        _context.Update(entity);

        if (saveChanges)
            await SaveChangesAsync();

        return entity;
    }

    #endregion Update Methods

    #region Hard Delete Methods

    /// <inheritdoc/>
    public async Task HardDeleteAsync(TEntity entity, bool saveChanges = true)
    {
        _context.Remove(entity);

        if (saveChanges)
            await SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task HardDeleteAsync(Guid id, bool saveChanges = true)
    {
        var entity = await _table.FindAsync(id);

        if (entity != null)
            _context.Remove(entity);

        if (saveChanges)
            await SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task HardDeleteMatchingAsync(IEnumerable<Guid> ids, bool saveChanges = true)
    {
        foreach (var id in ids)
        {
            var entity = await _table.FindAsync(id);
            if (entity != null)
                _context.Remove(entity);
        }

        if (saveChanges)
            await SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task HardDeleteMatchingAsync(bool saveChanges = true, params TEntity[] entities)
    {
        _context.RemoveRange(entities);

        if (saveChanges)
            await SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task HardDeleteMatchingAsync(IEnumerable<TEntity> entities, bool saveChanges = true)
    {
        _context.RemoveRange(entities);

        if (saveChanges)
            await SaveChangesAsync();
    }

    #endregion Hard Delete Methods

    #region Soft Delete Methods

    /// <inheritdoc/>
    public async Task SoftDeleteAsync(TEntity entity, bool saveChanges = true)
    {
        (entity as BaseEntity)!.IsDeleted = true;
        _context.Update(entity);

        if (saveChanges)
            await SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task SoftDeleteAsync(Guid id, bool saveChanges = true)
    {
        var entity = await _table.FindAsync(id);
        if (entity == null)
            return;

        (entity as BaseEntity)!.IsDeleted = true;
        _context.Update(entity);

        if (saveChanges)
            await SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task SoftDeleteMatchingAsync(IEnumerable<Guid> ids, bool saveChanges = true)
    {
        foreach (var id in ids)
        {
            var entity = await _table.FindAsync(id);
            if (entity == null)
                continue;

            (entity as BaseEntity)!.IsDeleted = true;
            _context.Update(entity);

            if (saveChanges)
                await SaveChangesAsync();
        }
    }

    /// <inheritdoc/>
    public async Task SoftDeleteMatchingAsync(bool saveChanges = true, params TEntity[] entities)
    {
        foreach (var entity in entities)
            (entity as BaseEntity)!.IsDeleted = true;
        _context.UpdateRange(entities.ToList());

        if (saveChanges)
            await SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task SoftDeleteMatchingAsync(IEnumerable<TEntity> entities, bool saveChanges = true)
    {
        var enumerable = entities as TEntity[] ?? entities.ToArray();
        foreach (var entity in enumerable)
            (entity as BaseEntity)!.IsDeleted = true;
        _context.UpdateRange(enumerable.ToList());

        if (saveChanges)
            await SaveChangesAsync();
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