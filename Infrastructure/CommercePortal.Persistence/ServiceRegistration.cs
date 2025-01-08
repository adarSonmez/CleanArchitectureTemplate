using CommercePortal.Application.Abstractions.Repositories.Files;
using CommercePortal.Application.Abstractions.Repositories.Marketing;
using CommercePortal.Application.Abstractions.Repositories.Membership;
using CommercePortal.Application.Abstractions.Repositories.Ordering;
using CommercePortal.Application.Abstractions.Services.Data;
using CommercePortal.Persistence.Contexts;
using CommercePortal.Persistence.Identity;
using CommercePortal.Persistence.Repositories.EntityFramework.Files;
using CommercePortal.Persistence.Repositories.EntityFramework.Marketing;
using CommercePortal.Persistence.Repositories.EntityFramework.Membership;
using CommercePortal.Persistence.Repositories.EntityFramework.Ordering;
using CommercePortal.Persistence.Services.Data.EntityFramework;
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

        services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            })
            .AddEntityFrameworkStores<EfDbContext>();
        //.AddDefaultTokenProviders();

        #endregion Identity services

        #region Repository services

        // Files
        services.AddScoped<ICategoryImageFileReadRepository, EfCategoryImageFileReadRepository>();
        services.AddScoped<ICategoryImageFileWriteRepository, EfCategoryImageFileWriteRepository>();
        services.AddScoped<IFileDetailsReadRepository, EfFileDetailsReadRepository>();
        services.AddScoped<IFileDetailsWriteRepository, EfFileDetailsWriteRepository>();
        services.AddScoped<IInvoiceFileReadRepository, EfInvoiceFileReadRepository>();
        services.AddScoped<IInvoiceFileWriteRepository, EfInvoiceFileWriteRepository>();
        services.AddScoped<IProductImageFileReadRepository, EfProductImageFileReadRepository>();
        services.AddScoped<IProductImageFileWriteRepository, EfProductImageFileWriteRepository>();
        services.AddScoped<IReportFileReadRepository, EfReportFileReadRepository>();
        services.AddScoped<IReportFileWriteRepository, EfReportFileWriteRepository>();
        services.AddScoped<IUserAvatarFileReadRepository, EfUserAvatarFileReadRepository>();
        services.AddScoped<IUserAvatarFileWriteRepository, EfUserAvatarFileWriteRepository>();

        // Marketing
        services.AddScoped<ICategoryReadRepository, EfCategoryReadRepository>();
        services.AddScoped<ICategoryWriteRepository, EfCategoryWriteRepository>();
        services.AddScoped<IProductReadRepository, EfProductReadRepository>();
        services.AddScoped<IProductWriteRepository, EfProductWriteRepository>();

        // Membership
        services.AddScoped<ICustomerReadRepository, EfCustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository, EfCustomerWriteRepository>();
        services.AddScoped<IStoreReadRepository, EfStoreReadRepository>();
        services.AddScoped<IStoreWriteRepository, EfStoreWriteRepository>();

        // Ordering
        services.AddScoped<IInvoiceReadRepository, EfInvoiceReadRepository>();
        services.AddScoped<IInvoiceWriteRepository, EfInvoiceWriteRepository>();
        services.AddScoped<IOrderItemReadRepository, EfOrderItemReadRepository>();
        services.AddScoped<IOrderItemWriteRepository, EfOrderItemWriteRepository>();
        services.AddScoped<IOrderReadRepository, EfOrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, EfOrderWriteRepository>();

        #endregion Repository services

        #region Custom services

        services.AddScoped<IDataService, EfDataService>();

        #endregion Custom services

        #region Mapping services

        services.AddAutoMapper(typeof(IdentityMappingProfile).Assembly, typeof(Application.ServiceRegistration).Assembly);

        #endregion Mapping services
    }
}