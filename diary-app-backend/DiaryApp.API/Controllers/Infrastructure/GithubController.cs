using DiaryApp.API.Requests;
using DiaryApp.Infrastructure.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DiaryApp.API.Controllers.Infrastructure
{
    public class GithubController : DiaryAppController
    {
        private readonly IGithubService _githubService;
        public GithubController(IGithubService githubService) 
        {
            _githubService = githubService;
        }

        [HttpPost("issues/bug")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateBugAsync(CreateIssueRequest createIssueRequest)
        {
            await _githubService.CreateBugIssue(createIssueRequest.UserName, createIssueRequest.Title, createIssueRequest.Description);
            return Ok();
        }

        [HttpPost("issues/feature")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateFeatureAsync(CreateIssueRequest createIssueRequest)
        {
            await _githubService.CreateFeatureIssue(createIssueRequest.UserName, createIssueRequest.Title, createIssueRequest.Description);
            return Ok();
        }
    }
}
