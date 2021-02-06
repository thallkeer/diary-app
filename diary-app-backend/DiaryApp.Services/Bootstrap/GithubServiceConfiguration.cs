using DiaryApp.Services.Implementations;
using DiaryApp.Services.ServiceInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DiaryApp.Services.Bootstrap
{
    public static class GithubServiceConfiguration
    {
        public static void AddGithubService(this IServiceCollection services)
        {
            services.AddScoped<IGithubService, GithubService>();
        }
    }
}
