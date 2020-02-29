using System;
using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using DiaryApp.Core.Models;
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
        public async Task<IActionResult> TransferPageDataToNextMonth(int prevPageID, TransferDataModel transferDataModel)
        {            
            if (transferDataModel == null)
                return BadRequest("No transfer information is received, check transfer data model");
            try
            {
                MonthPage prevPage = await monthPageService.GetById(prevPageID);
                if (prevPage == null)
                    return BadRequest("No original page received, check month page ID");
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
                var monthPage = new MonthPage()
                {
                    UserID = pageParams.UserId,
                    Year = pageParams.Year,
                    Month = pageParams.Month
                };

                await monthPageService.Create(monthPage);

                monthPage.DesiresArea = new DesiresArea(monthPage);
                monthPage.GoalsArea = new GoalsArea(monthPage);
                monthPage.PurchasesArea = new PurchasesArea(monthPage);
                monthPage.IdeasArea = new IdeasArea(monthPage);

                await monthPageService.Update(monthPage);

                monthPage.PurchasesArea.PurchasesLists.AddRange(new TodoList[]
                {
                new TodoList {Title = "Название списка"},
                new TodoList {Title = "Название списка"}
                });

                monthPage.PurchasesArea.PurchasesLists.ForEach(x => x.Page = monthPage);

                monthPage.DesiresArea.DesiresLists.AddRange(new EventList[]
                {
                new EventList {Title = "Прочитать"},
                new EventList {Title = "Посмотреть"},
                new EventList {Title = "Посетить"},
                });

                monthPage.DesiresArea.DesiresLists.ForEach(x => x.Page = monthPage);

                monthPage.IdeasArea.IdeasList = new EventList() { Page = monthPage };
                monthPage.GoalsArea.GoalsLists.Add(new HabitsTracker() { GoalName = "Название цели" });

                await monthPageService.Update(monthPage);

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
            try
            {
                var area = await monthPageService.GetPageArea<TEntity>(pageID);
                if (area == null)
                {
                    logger.LogError($"Page area {typeof(TEntity).FullName} not found for pageID {pageID}");
                    return NotFound();
                }
                return Ok(mapper.Map<TDto>(area));
            }
            catch (Exception ex)
            {
                logger.LogErrorWithDate(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}