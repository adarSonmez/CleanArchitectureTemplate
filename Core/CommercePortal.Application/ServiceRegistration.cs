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
        # region FluentValidation

        //services.AddFluentValidationAutoValidation();
        //services.AddFluentValidationClientsideAdapters();
        //services.AddValidatorsFromAssembly(typeof(ServiceRegistration).Assembly);

        # endregion FluentValidation

        # region MediatR

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));

        # endregion MediatR
    }
}