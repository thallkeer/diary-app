using AutoMapper;
using DiaryApp.Models.Requests;
using DiaryApp.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DiaryApp.API.Controllers.Infrastructure
{
    public class GithubController : AppBaseController<GithubController>
    {
        private readonly IGithubService _githubService;
        public GithubController(IGithubService githubService, IMapper mapper, ILoggerFactory loggerFactory) : base(mapper, loggerFactory)
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
        public async Task<IActionResult> CreateEnhancementAsync(CreateIssueRequest createIssueRequest)
        {
            await _githubService.CreateFeatureIssue(createIssueRequest.UserName, createIssueRequest.Title, createIssueRequest.Description);
            return Ok();
        }
    }
}
