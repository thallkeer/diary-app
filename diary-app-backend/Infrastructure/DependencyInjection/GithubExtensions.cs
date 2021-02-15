using DiaryApp.Infrastructure.ServiceInterfaces;
using DiaryApp.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Octokit;

namespace DiaryApp.Infrastructure.DependencyInjection
{
    public static class GithubExtensions
    {
        public static IServiceCollection AddGithubService(this IServiceCollection services, string githubToken)
        {
            var productHeader = new ProductHeaderValue("diary-app");
            var credentials = new Credentials(githubToken);
            var client = new GitHubClient(productHeader)
            {
                Credentials = credentials
            };
            return services.AddSingleton<IGitHubClient>(client)
                           .AddScoped<IGithubService, GithubService>();
        }
    }
}
