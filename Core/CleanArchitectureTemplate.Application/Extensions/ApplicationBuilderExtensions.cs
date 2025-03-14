﻿using CleanArchitectureTemplate.Application.Abstractions.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureTemplate.Application.Extensions;

/// <summary>
/// Extension methods for the <see cref="IApplicationBuilder"/> interface.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Migrates the database to the latest version.
    /// </summary>
    public static async Task UseMigratorAsync(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var migrator = services.GetRequiredService<IDataService>();
        await migrator.MigrateAsync();
    }

    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    public static async Task UseSeederAsync(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var seeder = services.GetRequiredService<IDataService>();
        await seeder.SeedAsync();
    }
}