using System.Threading.Tasks;
using DiaryApp.API.Models;
using DiaryApp.Core.Entities;
using DiaryApp.Core.Entities.PageAreas;
using DiaryApp.Services.DataInterfaces;
using DiaryApp.Services.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiaryApp.API.Controllers.Pages
{
    public class MonthPageController : PageController<MonthPageDto, MonthPage>
    {
        private readonly IMonthPageService _monthPageService;

        public MonthPageController(IMonthPageService monthPageService)
            : base(monthPageService)
        {
            _monthPageService = monthPageService;
        }

        [HttpGet("purchasesArea/{pageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PurchasesAreaDto>> GetPurchasesAreaAsync(int pageId)
        {
            var area = await GetPageArea<PurchasesArea, PurchasesAreaDto>(pageId);
            return area;
        }

        [HttpGet("desiresArea/{pageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DesiresAreaDto>> GetDesiresAreaAsync(int pageId)
        {
            return await GetPageArea<DesiresArea, DesiresAreaDto>(pageId);
        }

        [HttpGet("ideasArea/{pageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IdeasAreaDto>> GetIdeasAreaAsync(int pageId)
        {
            var model = await GetPageArea<IdeasArea, IdeasAreaDto>(pageId);
            return model;
        }

        [HttpGet("goalsArea/{pageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GoalsAreaDto>> GetGoalsAreaAsync(int pageId)
        {
            return await GetPageArea<GoalsArea, GoalsAreaDto>(pageId);
        }

        [HttpPost("transferData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostTransferPageDataAsync(TransferDataRequest transferDataRequestParams)
        {
            await _monthPageService.TransferPageDataToNextMonthAsync(
                transferDataRequestParams.OriginalPageId, transferDataRequestParams.TransferDataModel);
            return Ok();
        }
    }
}