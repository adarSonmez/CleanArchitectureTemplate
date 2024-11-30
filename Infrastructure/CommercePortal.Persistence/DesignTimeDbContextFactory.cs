using CommercePortal.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CommercePortal.Persistence;

/// <summary>
/// Represents the design time context factory for the entity framework.
/// </summary>
/// <remarks>This class is created in order to apply migrations from the command line.</remarks>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EfDbContext>
{
    public EfDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EfDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=CommercePortal;Username=postgres;Password=****");

        return new EfDbContext(optionsBuilder.Options);
    }
}