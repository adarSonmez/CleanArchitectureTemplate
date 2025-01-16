using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Abstractions.Services.Storage;
using CleanArchitectureTemplate.Infrastructure.Services.Storage;
using CleanArchitectureTemplate.Infrastructure.Services.Token.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureTemplate.Infrastructure;

/// <summary>
/// Represents the service registration for the infrastructure layer.
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Adds the infrastructure services to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<ITokenService, JwtTokenService>();
    }

    /// <summary>
    /// Adds the storage to the service collection.
    /// </summary>
    /// <typeparam name="T">The type of storage to add.</typeparam>
    /// <param name="services">The service collection to add the storage to.</param>
    public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
    {
        services.AddScoped<IStorage, T>();
    }
}