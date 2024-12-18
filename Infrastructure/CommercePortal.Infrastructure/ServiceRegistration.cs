using CommercePortal.Application.Abstractions.Storage;
using CommercePortal.Application.Abstractions.Token;
using CommercePortal.Infrastructure.Services.Storage;
using CommercePortal.Infrastructure.Services.Token.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace CommercePortal.Infrastructure;

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
        services.AddScoped<ITokenHandler, JwtTokenHandler>();
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