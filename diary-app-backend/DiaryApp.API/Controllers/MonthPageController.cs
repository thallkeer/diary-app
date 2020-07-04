using System;
using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using DiaryApp.Core.Models.PageAreas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiaryApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MonthPageController : AppBaseController<MonthPageController>
    {
        private readonly IMonthPageService monthPageService;

        public MonthPageController(IMonthPageService monthPageService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(mapper, loggerFactory)
        {
            this.monthPageService = monthPageService;
        }

        [HttpGet("{userId}/{year}/{month}")]
        public async Task<IActionResult> GetMonthPage(int userId, int year, int month)
        {
            try
            {
                var monthPage = await monthPageService.GetPageForUser(userId, year, month);

                if (monthPage == null)
                    return await CreateNewPage(new PageParams { UserId = userId, Year = year, Month = month });

                var model = mapper.Map<MonthPageModel>(monthPage);

                return Ok(model);
            }
            catch (Exception ex)
            {
                logger.LogErrorWithDate(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("transferData")]
        public async Task<IActionResult> TransferPageDataToNextMonth(TransferDataRequestParams transferDataRequestParams)
        {
            var prevPageParams = transferDataRequestParams.PageParams;
            var transferDataModel = transferDataRequestParams.TransferDataModel;
            if (prevPageParams == null)
                return BadRequest("No original page data received, check application state");
            if (transferDataModel == null)
                return BadRequest("No transfer information is received, check transfer data model");
            try
            {
                MonthPage prevPage = await monthPageService.GetPageForUser(prevPageParams.UserId, prevPageParams.Year, prevPageParams.Month);
                if (prevPage == null)
                    return BadRequest($"No original page found for {prevPageParams.UserId} {prevPageParams.Year} {prevPageParams.Month}");
                await monthPageService.TransferPageDataToNextMonth(prevPage, transferDataModel);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogErrorWithDate(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("createNew")]
        public async Task<IActionResult> CreateNewPage(PageParams pageParams)
        {
            try
            {
                MonthPage monthPage = await monthPageService.CreatePageByParams(pageParams.UserId,pageParams.Year, pageParams.Month);

                MonthPageModel model = mapper.Map<MonthPageModel>(monthPage);
                return Ok(model);
            }
            catch (Exception ex)
            {
                logger.LogErrorWithDate(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }       

        [HttpGet("purchasesArea/{pageID}")]
        public async Task<IActionResult> GetPurchasesArea(int pageID)
        {
            return await GetPageArea<PurchasesArea, PurchasesAreaModel>(pageID);
        }

        [HttpGet("desiresArea/{pageID}")]
        public async Task<IActionResult> GetDesiresArea(int pageID)
        {
            return await GetPageArea<DesiresArea, DesiresAreaModel>(pageID);
        }

        [HttpGet("ideasArea/{pageID}")]
        public async Task<IActionResult> GetIdeasArea(int pageID)
        {
            var model = await GetPageArea<IdeasArea, IdeasAreaModel>(pageID);
            return model;
        }

        [HttpGet("goalsArea/{pageID}")]
        public async Task<IActionResult> GetGoalsArea(int pageID)
        {
            return await GetPageArea<GoalsArea, GoalsAreaModel>(pageID);
        }

        private async Task<IActionResult> GetPageArea<TEntity, TDto>(int pageID)
            where TDto : PageAreaModel
            where TEntity : PageAreaBase
        {
            TEntity area;
            try
            {
                area = await monthPageService.GetPageArea<TEntity>(pageID);
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