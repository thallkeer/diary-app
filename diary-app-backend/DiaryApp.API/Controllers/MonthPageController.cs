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
    public class MonthPageController : PageController<MonthPage>
    {
        private readonly IMonthPageService monthPageService;

        public MonthPageController(IMonthPageService monthPageService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(monthPageService,mapper, loggerFactory)
        {
            this.monthPageService = monthPageService;
        }

        [HttpGet("{userId}/{year}/{month}")]
        public async Task<IActionResult> GetMonthPage(int userId, int year, int month)
        {
            return await GetPage(userId, year, month);
        }       

        [HttpPost("createNew")]
        public override async Task<IActionResult> CreateNewPage(PageParams pageParams)
        {
            return await base.CreateNewPage(pageParams);
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
    }
}