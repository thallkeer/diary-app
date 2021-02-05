using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiaryApp.Core.Bootstrap
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPostgresContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationContext>(options =>
                     options.UseLazyLoadingProxies()
                            .UseNpgsql(connectionString));
        }
    }
}
