using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommercePortal.Application;

/// <summary>
/// Represents the service registration for the application layer.
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Adds the application services to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    public static void AddApplicationServices(this IServiceCollection services)
    {
        # region MediatR

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));

        # endregion MediatR
    }
}