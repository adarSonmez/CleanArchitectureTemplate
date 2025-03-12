using CleanArchitectureTemplate.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CleanArchitectureTemplate.Persistence;

/// <summary>
/// Represents the design time context factory for the entity framework.
/// </summary>
/// <remarks>This class is created in order to apply migrations from the command line.</remarks>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EfDbContext>
{
    public EfDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../CleanArchitectureTemplate.WebAPI"))
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<EfDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new EfDbContext(optionsBuilder.Options, default!);
    }
}