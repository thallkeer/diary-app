using System.Threading.Tasks;
using DiaryApp.API.Requests;
using DiaryApp.Core.Entities;
using DiaryApp.Core.Entities.Pages;
using DiaryApp.Core.Interfaces;
using DiaryApp.Services.DataInterfaces;
using DiaryApp.Services.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiaryApp.API.Controllers.Pages
{
    public class PageController<TPageDto, TPage> : DiaryAppController
        where TPageDto : PageDto
        where TPage : PageBase
    {
        protected readonly IPageService<TPageDto, TPage> pageService;

        public PageController(IPageService<TPageDto, TPage> pageService)
            
        {
            this.pageService = pageService;
        }

        [HttpGet("{userId}/{year}/{month}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TPageDto>> GetPageAsync(int userId, int year, int month)
        {
            var page = await pageService.GetPageAsync(userId, year, month);
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
            var page = await pageService.CreateAsync(pageParams.UserId, pageParams.Year, pageParams.Month);
            return Ok(page);
        }

        protected async Task<ActionResult<TPageAreaDto>> GetPageArea<TPageArea, TPageAreaDto>(int pageID)
            where TPageArea : class, IPageArea
            where TPageAreaDto : PageAreaDto
        {
            var area = await pageService.GetPageAreaOrThrowAsync<TPageArea>(pageID);
            return Ok(Mapper.Map<TPageAreaDto>(area));
        }
    }
}