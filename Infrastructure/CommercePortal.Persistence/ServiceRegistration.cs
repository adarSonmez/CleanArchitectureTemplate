using CommercePortal.Application.Repositories;
using CommercePortal.Persistence.Contexts;
using CommercePortal.Persistence.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommercePortal.Persistence
{
    /// <summary>
    /// Represents the service registration for the persistence layer.
    /// </summary>
    public static class ServiceRegistration
    {
        /// <summary>
        /// Adds the persistence services to the service collection.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            #region Database provider

            services.AddDbContext<EfDbContext>(options => options.UseNpgsql("Host=localhost;Port=5432;Database=CommercePortal;Username=postgres;Password=sneider14"), ServiceLifetime.Singleton);

            #endregion Database provider

            #region Repository services

            services.AddSingleton<IProductReadRepository, EfProductReadRepository>();
            services.AddSingleton<IProductWriteRepository, EfProductWriteRepository>();
            services.AddSingleton<IOrderReadRepository, EfOrderReadRepository>();
            services.AddSingleton<IOrderWriteRepository, EfOrderWriteRepository>();
            services.AddSingleton<ICustomerReadRepository, EfCustomerReadRepository>();
            services.AddSingleton<ICustomerWriteRepository, EfCustomerWriteRepository>();

            #endregion Repository services
        }
    }
}