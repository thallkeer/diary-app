using AutoMapper;
using DiaryApp.API.Exceptions;
using DiaryApp.API.Models;
using DiaryApp.Models.DTO;
using DiaryApp.Core.Interfaces;
using DiaryApp.Data.Exceptions;
using DiaryApp.Data.DataInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DiaryApp.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace DiaryApp.API.Controllers
{
    public class PageController<TPageDto, TPage> : AppBaseController<PageController<TPageDto, TPage>>
        where TPageDto : PageDto
        where TPage : PageBase
    {
        protected readonly IPageService<TPageDto, TPage> pageService;

        public PageController(IPageService<TPageDto, TPage> pageService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(mapper, loggerFactory)
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

            return Ok(mapper.Map<TPageDto>(page));
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
                logger.LogError(err);
                return NotFound(err);
            }
            return Ok(mapper.Map<TPageAreaDto>(area));
        }
    }
}