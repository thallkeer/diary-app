using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Services.DTO;
using DiaryApp.Core.Interfaces;
using DiaryApp.Services.Exceptions;
using DiaryApp.Services.DataInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DiaryApp.Core.Entities;

namespace DiaryApp.API.Controllers
{
    public class PageController<TPageDto, TPage> : DiaryAppContoller
        where TPageDto : PageDto
        where TPage : PageBase
    {
        protected readonly IPageService<TPageDto, TPage> pageService;

        public PageController(IPageService<TPageDto, TPage> pageService, IMapper mapper)
            : base(mapper)
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

            return Ok(_mapper.Map<TPageDto>(page));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<TPageDto>> CreatePageAsync(PageRequest pageParams)
        {
            TPageDto page;
            try
            {
                page = await pageService.CreateAsync(pageParams.UserId, pageParams.Year, pageParams.Month);
            }
            catch (PageAlreadyExistsException e)
            {
                throw new HttpException(System.Net.HttpStatusCode.Conflict, e.Message);
            }
            catch (UserNotExistsException ex)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
            return Ok(page);
        }

        protected async Task<ActionResult<TPageAreaDto>> GetPageArea<TPageArea, TPageAreaDto>(int pageID)
            where TPageArea : class, IPageArea
            where TPageAreaDto : PageAreaDto
        {
            TPageArea area = await pageService.GetPageArea<TPageArea>(pageID);
            if (area == null)
            {
                string err = $"{typeof(TPageArea).Name} not found for pageID {pageID}";
                Logger.Error(err);
                return NotFound(err);
            }
            return Ok(_mapper.Map<TPageAreaDto>(area));
        }
    }
}