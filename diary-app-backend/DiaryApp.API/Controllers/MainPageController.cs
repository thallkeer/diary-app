using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DiaryApp.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using DiaryApp.Core.Models.PageAreas;
using DiaryApp.Data.ServiceInterfaces;
using DiaryApp.Core.DTO;

namespace DiaryApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MainPageController : PageController<MainPageDto, IMainPageService>
    {
        public MainPageController(IMainPageService mainPageService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(mainPageService, mapper, loggerFactory)
        {
        }

        [HttpGet("{userId}/{year}/{month}")]
        public async Task<IActionResult> GetMainPage(int userId, int year, int month)
        {
            return await GetPage(userId, year, month);
        }

        [HttpPost]
        public override async Task<IActionResult> CreateNewPage(PageParams pageParams)
        {
            return await base.CreateNewPage(pageParams);
        }

        [HttpGet("importantThingsArea/{pageID}")]
        public async Task<IActionResult> GetImportantThingsArea(int pageID)
        {
            var model = await GetPageArea<ImportantThingsArea, ImportantThingsAreaDto>(pageID);
            return model;
        }

        [HttpGet("importantEventsArea/{pageID}")]
        public async Task<IActionResult> GetImportantEventsArea(int pageID)
        {
            return await GetPageArea<ImportantEventsArea, ImportantEventsAreaDto>(pageID);
        }
    }
}