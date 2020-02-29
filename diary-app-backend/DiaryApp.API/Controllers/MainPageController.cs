using System.Threading.Tasks;
using DiaryApp.Core;
using Microsoft.AspNetCore.Mvc;
using DiaryApp.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DiaryApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MainPageController : AppBaseController<MainPageController>
    {
        private readonly IMainPageService mainPageService;
        private readonly IEventService eventService;
        private readonly ITodoService todoService;

        public MainPageController(IMainPageService mainPageService, IEventService eventService, ITodoService todoService,
            IMapper mapper, ILoggerFactory loggerFactory)
            : base(mapper, loggerFactory)
        {
            this.mainPageService = mainPageService;
            this.eventService = eventService;
            this.todoService = todoService;
        }

        [HttpGet("{userId}/{year}/{month}")]
        public async Task<IActionResult> GetMainPage(int userId, int year, int month)
        {
            try
            {
                var mainPage = await mainPageService.GetPageForUser(userId, year, month);

                if (mainPage == null)
                    return await CreateNewPage(new PageParams { UserId = userId, Year = year, Month = month });

                var model = mapper.Map<MainPageModel>(mainPage);                
                //model.ImportantEvents.Items = model.ImportantEvents.Items.OrderBy(e => e.Date).ToList();
                return Ok(model);
            }
            catch (Exception ex)
            {
                logger.LogErrorWithDate(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createNew")]
        public async Task<IActionResult> CreateNewPage(PageParams pageParams)
        {
            try
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

                MainPageModel model = mapper.Map<MainPageModel>(mainPage);
                return Ok(model);
            }
            catch (Exception ex)
            {
                logger.LogErrorWithDate(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}