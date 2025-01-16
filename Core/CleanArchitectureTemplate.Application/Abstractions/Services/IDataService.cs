using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Abstractions.Services;

/// <summary>
/// Represents the data service interface for seeding and updating the database.
/// </summary>
public interface IDataService : IService
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    Task SeedAsync();

    /// <summary>
    /// Migrates the database to the latest version.
    /// </summary>
    void Migrate();
}