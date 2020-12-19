using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DiaryApp.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using DiaryApp.Core.Models.PageAreas;
using DiaryApp.Data.ServiceInterfaces;
using DiaryApp.Core.DTO;
using Microsoft.AspNetCore.Http;

namespace DiaryApp.API.Controllers
{
    public class MainPageController : PageController<MainPageDto>
    {
        public MainPageController(IMainPageService mainPageService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(mainPageService, mapper, loggerFactory)
        {
        }

        [HttpGet("importantThingsArea/{pageID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetImportantThingsAreaAsync(int pageID)
        {
            var model = await GetPageArea<ImportantThingsArea, ImportantThingsAreaDto>(pageID);
            return model;
        }

        [HttpGet("importantEventsArea/{pageID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetImportantEventsAreaAsync(int pageID)
        {
            return await GetPageArea<ImportantEventsArea, ImportantEventsAreaDto>(pageID);
        }
    }
}