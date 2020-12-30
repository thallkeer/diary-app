using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Data.DTO;
using DiaryApp.Core.Models.PageAreas;
using DiaryApp.Data.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DiaryApp.Core;

namespace DiaryApp.API.Controllers
{
    public class MonthPageController : PageController<MonthPageDto, MonthPage>
    {
        private readonly IMonthPageService _monthPageService;

        public MonthPageController(IMonthPageService monthPageService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(monthPageService, mapper, loggerFactory)
        {
            _monthPageService = monthPageService;
        }

        [HttpGet("purchasesArea/{pageID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPurchasesAreaAsync(int pageID)
        {
            return await GetPageArea<PurchasesArea, PurchasesAreaDto>(pageID);
        }

        [HttpGet("desiresArea/{pageID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDesiresAreaAsync(int pageID)
        {
            return await GetPageArea<DesiresArea, DesiresAreaDto>(pageID);
        }

        [HttpGet("ideasArea/{pageID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetIdeasAreaAsync(int pageID)
        {
            var model = await GetPageArea<IdeasArea, IdeasAreaDto>(pageID);
            return model;
        }

        [HttpGet("goalsArea/{pageID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGoalsAreaAsync(int pageID)
        {
            return await GetPageArea<GoalsArea, GoalsAreaDto>(pageID);
        }

        [HttpPost("transferData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostTransferPageDataAsync(TransferDataRequestParams transferDataRequestParams)
        {
            var prevPageParams = transferDataRequestParams.PageParams;
            var transferDataModel = transferDataRequestParams.TransferDataModel;
            if (prevPageParams == null)
                return BadRequest("No original page data received, check application state");
            if (transferDataModel == null)
                return BadRequest("No transfer information is received, check transfer data model");

            MonthPage prevPage = await pageService.GetPageAsync(prevPageParams.UserId, prevPageParams.Year, prevPageParams.Month);
            if (prevPage == null)
                return BadRequest($"No original page found for {prevPageParams.UserId} {prevPageParams.Year} {prevPageParams.Month}");

            //TODO: check this method and refactor

            await _monthPageService.TransferPageDataToNextMonthAsync(prevPage.Id, transferDataModel);
            return Ok();
        }
    }
}