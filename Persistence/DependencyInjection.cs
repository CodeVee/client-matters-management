using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Services;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<ApplicationContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
            //            b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlite("Data Source=ClientMatters.db",
                        b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            return services;
        }
    }
}
