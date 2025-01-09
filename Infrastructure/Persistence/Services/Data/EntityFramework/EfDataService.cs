using CleanArchitectureTemplate.Application.Abstractions.Services.Data;
using CleanArchitectureTemplate.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureTemplate.Persistence.Services.Data.EntityFramework;

/// <summary>
/// Represents the data service for seeding and updating the database based on Entity Framework.
/// </summary>
public class EfDataService : IDataService
{
    private readonly EfDbContext _context;

    public EfDataService(EfDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public void Seed()
    {
        // If there is no data in the database, then seed the database
        if (_context.Categories.Any())
            return;
    }

    /// <inheritdoc/>
    public void Migrate()
    {
        if (_context.Database.GetPendingMigrations().Any())
            _context.Database.Migrate();
    }
}