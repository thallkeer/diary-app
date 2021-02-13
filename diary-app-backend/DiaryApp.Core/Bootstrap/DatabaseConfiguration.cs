using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiaryApp.Core.Bootstrap
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddPostgresContext(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContext<ApplicationContext>(options =>
                     options.UseLazyLoadingProxies()
                            .UseNpgsql(connectionString));
        }
    }
}
