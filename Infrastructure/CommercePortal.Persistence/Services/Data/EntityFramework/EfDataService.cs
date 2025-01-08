using CommercePortal.Application.Abstractions.Services.Data;
using CommercePortal.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CommercePortal.Persistence.Services.Data.EntityFramework;

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