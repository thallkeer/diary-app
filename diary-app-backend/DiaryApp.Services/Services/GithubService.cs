using System;
using System.Threading.Tasks;
using DiaryApp.Services.ServiceInterfaces;
using Octokit;

namespace DiaryApp.Services.Implementations
{
    public class GithubService : IGithubService
    {
        private const string OWNER_NAME = "thallkeer";
        private const string REPO_NAME = "diary-app";

        private readonly IGitHubClient _client;

        public GithubService(IGitHubClient gitHubClient)
        {
            _client = gitHubClient;
        }

        public async Task CreateFeatureIssue(string userName, string title, string description)
        {
            await CreateIssue(userName, title, description, "new feature", "enhancement");
        }

        public async Task CreateBugIssue(string userName, string title, string description)
        {
            await CreateIssue(userName, title, description, "bug", "invalid");
        }

        private async Task CreateIssue(string userName, string title, string description, params string[] labels)
        {
            string descriptionFooter = string.IsNullOrEmpty(userName) ? string.Empty : $"Пользователь: {userName}";
            var descriptionWithFooter = $"{description}\n{descriptionFooter}".Trim();
            var createIssue = new NewIssue(title)
            {
                Body = descriptionWithFooter
            };
            var issue = await _client.Issue.Create(OWNER_NAME, REPO_NAME, createIssue);

            var updateIssue = issue.ToUpdate();
            updateIssue.AddAssignee(OWNER_NAME);
            Array.ForEach(labels, label => updateIssue.AddLabel(label));
            await _client.Issue.Update(OWNER_NAME, REPO_NAME, issue.Number, updateIssue);
        }
    }
}
