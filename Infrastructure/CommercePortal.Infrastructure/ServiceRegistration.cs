using CommercePortal.Application.Services;
using CommercePortal.Infrastructure.Services;
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
        services.AddScoped<IFileService, FileService>();
    }
}