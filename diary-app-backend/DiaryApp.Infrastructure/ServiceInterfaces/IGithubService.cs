using System.Threading.Tasks;

namespace DiaryApp.Infrastructure.ServiceInterfaces
{
    public interface IGithubService
    {
        /// <summary>
        /// Creates new issue marked as bug
        /// </summary>
        /// <param name="userName">User created an issue</param>
        /// <param name="title">Title of issue</param>
        /// <param name="description">Body of issue</param>
        /// <returns></returns>
        Task CreateBugIssue(string userName, string title, string description);

        /// <summary>
        /// Creates new issue marked as feature
        /// </summary>
        /// <param name="userName">User created an issue</param>
        /// <param name="title">Title of issue</param>
        /// <param name="description">Body of issue</param>
        /// <returns></returns>
        Task CreateFeatureIssue(string userName, string title, string description);
    }
}
