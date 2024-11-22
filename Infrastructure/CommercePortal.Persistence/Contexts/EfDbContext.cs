using CommercePortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommercePortal.Persistence.Contexts
{
    /// <summary>
    /// Represents the entity framework implementation of the <see cref="DbContext"/>.
    /// </summary>
    public class EfDbContext : DbContext
    {
        #region DbSet Properties

        /// <summary>
        /// Gets or sets the products table.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the customers table.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Gets or sets the orders table.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        #endregion DbSet Properties

        #region Constructors

        public EfDbContext()
        {
        }

        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
        {
        }

        #endregion Constructors
    }
}