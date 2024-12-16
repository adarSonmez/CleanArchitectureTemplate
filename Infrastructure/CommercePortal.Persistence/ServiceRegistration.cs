using CommercePortal.Application.Repositories.Customers;
using CommercePortal.Application.Repositories.Files;
using CommercePortal.Application.Repositories.Orders;
using CommercePortal.Application.Repositories.Products;
using CommercePortal.Domain.Entities.Identity;
using CommercePortal.Persistence.Contexts;
using CommercePortal.Persistence.Repositories.EntityFramework.Customers;
using CommercePortal.Persistence.Repositories.EntityFramework.Files;
using CommercePortal.Persistence.Repositories.EntityFramework.Orders;
using CommercePortal.Persistence.Repositories.EntityFramework.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommercePortal.Persistence;

/// <summary>
/// Represents the service registration for the persistence layer.
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Adds the persistence services to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region Database provider

        services.AddDbContext<EfDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        #endregion Database provider

        #region Identity services

        services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<EfDbContext>();
        //.AddDefaultTokenProviders();

        #endregion Identity services

        #region Repository services

        services.AddScoped<IProductReadRepository, EfProductReadRepository>();
        services.AddScoped<IProductWriteRepository, EfProductWriteRepository>();
        services.AddScoped<IOrderReadRepository, EfOrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, EfOrderWriteRepository>();
        services.AddScoped<ICustomerReadRepository, EfCustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository, EfCustomerWriteRepository>();
        services.AddScoped<IFileReadRepository, EfFileReadRepository>();
        services.AddScoped<IFileWriteRepository, EfFileWriteRepository>();
        services.AddScoped<IInvoiceFileReadRepository, EfInvoiceFileReadRepository>();
        services.AddScoped<IInvoiceFileWriteRepository, EfInvoiceFileWriteRepository>();
        services.AddScoped<IProductImageFileReadRepository, EfProductImageFileReadRepository>();
        services.AddScoped<IProductImageFileWriteRepository, EfProductImageFileWriteRepository>();

        #endregion Repository services
    }
}