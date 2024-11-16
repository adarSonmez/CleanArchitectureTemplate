using CommercePortal.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommercePortal.Persistence
{
    public static class ServiceRegistation
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<EfDbContext>(options =>
            {
                options.UseNpgsql("Host=localhost;Port=5432;Database=CommercePortal;Username=postgres;Password=****");
            });
        }
    }
}