using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core.Models;
using DiaryApp.Data.DTO;
using DiaryApp.Data.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public async Task<ActionResult<PurchasesAreaDto>> GetPurchasesAreaAsync(int pageID)
        {
            var area = await GetPageArea<PurchasesArea, PurchasesAreaDto>(pageID);
            return area;
        }

        [HttpGet("desiresArea/{pageID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DesiresAreaDto>> GetDesiresAreaAsync(int pageID)
        {
            return await GetPageArea<DesiresArea, DesiresAreaDto>(pageID);
        }

        [HttpGet("ideasArea/{pageID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IdeasAreaDto>> GetIdeasAreaAsync(int pageID)
        {
            var model = await GetPageArea<IdeasArea, IdeasAreaDto>(pageID);
            return model;
        }

        [HttpGet("goalsArea/{pageID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GoalsAreaDto>> GetGoalsAreaAsync(int pageID)
        {
            return await GetPageArea<GoalsArea, GoalsAreaDto>(pageID);
        }

        [HttpPost("transferData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostTransferPageDataAsync(TransferDataRequestParams transferDataRequestParams)
        {
            await _monthPageService.TransferPageDataToNextMonthAsync(
                transferDataRequestParams.OriginalPageId, transferDataRequestParams.TransferDataModel);
            return Ok();
        }
    }
}