using System.Threading.Tasks;
using DiaryApp.Core;
using Microsoft.AspNetCore.Mvc;
using DiaryApp.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System;

namespace DiaryApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MainPageController : ControllerBase
    {
        private readonly IMainPageService mainPageService;
        private readonly IEventService eventService;
        private readonly ITodoService todoService;
        private readonly IMapper mapper;

        public MainPageController(IMainPageService mainPageService, IEventService eventService, ITodoService todoService, IMapper mapper)
        {
            this.mainPageService = mainPageService;
            this.eventService = eventService;
            this.todoService = todoService;
            this.mapper = mapper;
        }

        [HttpGet("{userId}/{year}/{month}")]
        public async Task<MainPageModel> GetMainPage(string userId, int year, int month)
        {
            var mainPage = await mainPageService.GetPageForUser(userId, year, month);

            var model = mapper.Map<MainPageModel>(mainPage);

            //model.ImportantEvents.Items = model.ImportantEvents.Items.OrderBy(e => e.Date).ToList();

            return model;
        }

        [HttpPost("createNew")]
        public async Task<IActionResult> CreateNewPage(PageParams pageParams)
        {
            var mainPage = new MainPage()
            {
                UserID = pageParams.UserId,
                Year = pageParams.Year,
                Month = pageParams.Month,
            };

            await mainPageService.Create(mainPage);

            var impEvents = new EventList()
            {
                Title = "Важные события",
                Page = mainPage
            };

            var todos = new TodoList
            {
                Title = "Важные дела",
                Page = mainPage
            };

            await eventService.Create(impEvents);
            await todoService.Create(todos);

            mainPage.ImportantEvents = impEvents;
            mainPage.ThingsTodo = todos;

            var model = mapper.Map<MainPageModel>(mainPage);

            return Ok(model);
        }
    }
}