using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core.DTO;
using DiaryApp.Core.Interfaces;
using DiaryApp.Data.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DiaryApp.API.Controllers
{
    public class PageController<TPageDto> : AppBaseController<PageController<TPageDto>>
        where TPageDto : PageDto
    {
        protected readonly IPageService<TPageDto> pageService;

        public PageController(IPageService<TPageDto> pageService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(mapper, loggerFactory)
        {
            this.pageService = pageService;
        }

        [HttpGet("{userId}/{year}/{month}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(int userId, int year, int month)
        {
            try
            {
                var page = await pageService.GetPageAsync(userId, year, month);

                if (page == null)
                    return await PostAsync(new PageParams { UserId = userId, Year = year, Month = month });

                return Ok(page);
            }
            catch (Exception ex)
            {
                logger.LogErrorWithDate(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PostAsync(PageParams pageParams)
        {
            try
            {
                TPageDto page = await pageService.CreateAsync(pageParams.UserId, pageParams.Year, pageParams.Month);

                return Ok(page);
            }
            catch (Exception ex)
            {
                logger.LogErrorWithDate(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        protected async Task<IActionResult> GetPageArea<TPageArea, TPageAreaDto>(int pageID)
            where TPageArea : class, IPageArea
            where TPageAreaDto : PageAreaDto
        {
            try
            {
                var area = await pageService.GetPageArea<TPageAreaDto, TPageArea>(pageID);
                if (area == null)
                {
                    string err = $"Page area {typeof(TPageArea).FullName} not found for pageID {pageID}";
                    logger.LogError(err);
                    return NotFound(err);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogErrorWithDate(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
