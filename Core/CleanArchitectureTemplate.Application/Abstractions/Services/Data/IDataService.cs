namespace CleanArchitectureTemplate.Application.Abstractions.Services.Data;

/// <summary>
/// Represents the data service interface for seeding and updating the database.
/// </summary>
public interface IDataService
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    Task Seed();

    /// <summary>
    /// Migrates the database to the latest version.
    /// </summary>
    void Migrate();
}