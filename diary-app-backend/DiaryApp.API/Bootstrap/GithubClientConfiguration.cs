using DiaryApp.API.Settings;
using Microsoft.Extensions.DependencyInjection;
using Octokit;

namespace DiaryApp.API.Bootstrap
{
    public static class GithubClientConfiguration
    {
        public static void AddGithubClient(this IServiceCollection services, AppSettings appSettings)
        {
            var productHeader = new ProductHeaderValue("diary-app");
            var credentials = new Credentials(appSettings.GithubToken);
            var client = new GitHubClient(productHeader)
            {
                Credentials = credentials
            };
            services.AddSingleton<IGitHubClient>(client);
        }
    }
}
