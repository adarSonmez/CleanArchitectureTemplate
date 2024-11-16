using CommercePortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommercePortal.Persistence.Contexts
{
    public class EfDbContext(DbContextOptions<EfDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}