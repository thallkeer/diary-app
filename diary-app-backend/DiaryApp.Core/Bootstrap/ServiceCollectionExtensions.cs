using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiaryApp.Core.Bootstrap
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSqlServerContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
                    options.UseLazyLoadingProxies()
                           .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void AddPostgresContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
                            options.UseLazyLoadingProxies()
                                   .UseNpgsql(configuration.GetConnectionString("ProdConnection")));
        }
    }
}
