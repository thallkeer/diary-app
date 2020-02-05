using System;
using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiaryApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MonthPageController : ControllerBase
    {
        private readonly IMonthPageService monthPageService;
        private readonly IMapper mapper;

        public MonthPageController(IMonthPageService monthPageService, IMapper mapper)
        {
            this.monthPageService = monthPageService;
            this.mapper = mapper;
        }

        [HttpPost("createNew")]
        public async Task<IActionResult> CreateNewPage(PageParams pageParams)
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
            monthPage.PurchasesArea =  new PurchasesArea(monthPage);
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

            monthPage.IdeasArea.IdeasList = new EventList() { Page = monthPage};
            monthPage.GoalsArea.GoalsLists.Add(new HabitsTracker() { GoalName = "Название цели" });

            await monthPageService.Update(monthPage);

            var model = mapper.Map<MonthPageModel>(monthPage);

            return Ok(model);
        }

        [HttpGet("{userId}/{year}/{month}")]
        public async Task<MonthPageModel> GetMonthPage(string userId, int year, int month)
        {
            //TODO: send some error when page is not founded
            var monthPage = await monthPageService.GetPageForUser(userId, year, month);

            var model = mapper.Map<MonthPageModel>(monthPage);

            return model;
        }

        [HttpGet("purchasesArea/{pageID}")]
        public async Task<PurchasesAreaModel> GetPurchasesArea(int pageID)
        {
            return await GetPageArea<PurchasesArea, PurchasesAreaModel>(pageID);
        }

        [HttpGet("desiresArea/{pageID}")]
        public async Task<DesiresAreaModel> GetDesiresArea(int pageID)
        {
            return await GetPageArea<DesiresArea, DesiresAreaModel>(pageID);
        }

        [HttpGet("ideasArea/{pageID}")]
        public async Task<IdeasAreaModel> GetIdeasArea(int pageID)
        {
            var model = await GetPageArea<IdeasArea, IdeasAreaModel>(pageID);
            return model;
        }

        [HttpGet("goalsArea/{pageID}")]
        public async Task<GoalsAreaModel> GetGoalsArea(int pageID)
        {
            return await GetPageArea<GoalsArea, GoalsAreaModel>(pageID);
        }

        private async Task<TDto> GetPageArea<TEntity, TDto>(int pageID)
            where TDto : PageAreaModel
            where TEntity : PageAreaBase
        {
            var area = await monthPageService.GetPageArea<TEntity>(pageID);
            return mapper.Map<TDto>(area);
        }
    }
}