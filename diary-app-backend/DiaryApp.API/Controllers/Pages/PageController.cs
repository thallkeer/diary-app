using System.Threading.Tasks;
using DiaryApp.API.Filters;
using DiaryApp.API.Requests;
using DiaryApp.Core.Entities.Pages;
using DiaryApp.Services.DataInterfaces.Pages;
using DiaryApp.Services.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiaryApp.API.Controllers.Pages
{
    public class PageController<TPageDto, TPage> : DiaryAppController
        where TPageDto : PageDto
        where TPage : PageBase
    {
        protected readonly IPageService<TPageDto, TPage> PageService;

        public PageController(IPageService<TPageDto, TPage> pageService)
        {
            PageService = pageService;
        }

        [HttpGet("{userId}/{year}/{month}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Cached(600)]
        public async Task<ActionResult<TPageDto>> GetPageAsync(int year, int month)
        {
            var page = await PageService.GetPageAsync(UserId, year, month);
            if (page == null)
                return NotFound();

            return Ok(Mapper.Map<TPageDto>(page));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<TPageDto>> CreatePageAsync(CreatePageRequest pageParams)
        {
            var page = await PageService.CreateAsync(pageParams.UserId, pageParams.Year, pageParams.Month);
            return Ok(page);
        }
    }
}