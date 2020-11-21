using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using DiaryApp.Core.Models.PageAreas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DiaryApp.API.Controllers
{
    public class PageController<TPage> : AppBaseController<PageController<TPage>>
        where TPage : PageBase
    {    
        private readonly IPageService<TPage> pageService;

        public PageController(IPageService<TPage> monthPageService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(mapper, loggerFactory)
        {
            this.pageService = monthPageService;
        }

        protected virtual async Task<IActionResult> GetPage(int userId, int year, int month)
        {
            try
            {
                var page = await pageService.GetPageForUser(userId, year, month);

                if (page == null)
                    return await CreateNewPage(new PageParams { UserId = userId, Year = year, Month = month });

                var model = mapper.Map<PageModel>(page);

                return Ok(model);
            }
            catch (Exception ex)
            {
                logger.LogErrorWithDate(ex);
                return BadRequest(ex.Message);
            }
        }

        public virtual async Task<IActionResult> CreateNewPage(PageParams pageParams)
        {
            try
            {
                TPage page = await pageService.CreatePageByParams(pageParams.UserId, pageParams.Year, pageParams.Month);

                PageModel model = mapper.Map<PageModel>(page);
                return Ok(model);
            }
            catch (Exception ex)
            {
                logger.LogErrorWithDate(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        protected async Task<IActionResult> GetPageArea<TEntity, TDto>(int pageID)
            where TEntity : PageAreaBase<TPage>
            where TDto : PageAreaModel
        {
            TEntity area;
            try
            {
                area = await pageService.GetPageArea<TEntity>(pageID);
                if (area == null)
                {
                    string err = $"Page area {typeof(TEntity).FullName} not found for pageID {pageID}";
                    logger.LogError(err);
                    return NotFound(err);
                }
                var model = mapper.Map<TDto>(area);
                return Ok(model);
            }
            catch (Exception ex)
            {
                logger.LogErrorWithDate(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
