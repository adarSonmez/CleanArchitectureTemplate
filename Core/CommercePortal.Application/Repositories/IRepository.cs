using CommercePortal.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace CommercePortal.Application.Repositories
{
    /// <summary>
    /// Represents a common interface for read and write repositories.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity inherited from <see cref="BaseEntity"/>.</typeparam>
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Gets the table of the entity type <typeparamref name="TEntity"/>.
        /// </summary>
        DbSet<TEntity> Table { get; set; }
    }
}