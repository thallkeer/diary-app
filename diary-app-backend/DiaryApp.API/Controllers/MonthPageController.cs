using System;
using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core.DTO;
using DiaryApp.Core.Models.PageAreas;
using DiaryApp.Data.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiaryApp.API.Controllers
{
    public class MonthPageController : PageController<MonthPageDto>
    {
        private readonly IMonthPageService _monthPageService;

        public MonthPageController(IMonthPageService monthPageService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(monthPageService,mapper, loggerFactory)
        {
            _monthPageService = monthPageService;
        }     

        [HttpGet("purchasesArea/{pageID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPurchasesAreaAsync(int pageID)
        {
            return await GetPageArea<PurchasesArea, PurchasesAreaDto>(pageID);
        }

        [HttpGet("desiresArea/{pageID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDesiresAreaAsync(int pageID)
        {
            return await GetPageArea<DesiresArea, DesiresAreaDto>(pageID);
        }

        [HttpGet("ideasArea/{pageID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetIdeasAreaAsync(int pageID)
        {
            var model = await GetPageArea<IdeasArea, IdeasAreaDto>(pageID);
            return model;
        }

        [HttpGet("goalsArea/{pageID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGoalsAreaAsync(int pageID)
        {
            return await GetPageArea<GoalsArea, GoalsAreaDto>(pageID);
        }

        [HttpPost("transferData")]
        public async Task<IActionResult> PostTransferPageDataAsync(TransferDataRequestParams transferDataRequestParams)
        {
            var prevPageParams = transferDataRequestParams.PageParams;
            var transferDataModel = transferDataRequestParams.TransferDataModel;
            if (prevPageParams == null)
                return BadRequest("No original page data received, check application state");
            if (transferDataModel == null)
                return BadRequest("No transfer information is received, check transfer data model");
            try
            {
                MonthPageDto prevPage = await pageService.GetPageAsync(prevPageParams.UserId, prevPageParams.Year, prevPageParams.Month);
                if (prevPage == null)
                    return BadRequest($"No original page found for {prevPageParams.UserId} {prevPageParams.Year} {prevPageParams.Month}");
                await _monthPageService.TransferPageDataToNextMonthAsync(prevPage, transferDataModel);
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