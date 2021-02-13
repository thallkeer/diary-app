using DiaryApp.API.Settings;
using DiaryApp.Services.Implementations;
using DiaryApp.Services.ServiceInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Octokit;

namespace DiaryApp.API.Bootstrap
{
    public static class GithubExtensions
    {
        public static IServiceCollection AddGithubService(this IServiceCollection services, AppSettings appSettings)
        {
            var productHeader = new ProductHeaderValue("diary-app");
            var credentials = new Credentials(appSettings.GithubToken);
            var client = new GitHubClient(productHeader)
            {
                Credentials = credentials
            };
            return services.AddSingleton<IGitHubClient>(client)
                           .AddScoped<IGithubService, GithubService>();
        }
    }
}
