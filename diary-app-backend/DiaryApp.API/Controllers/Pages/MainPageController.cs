using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Logging;
using DiaryApp.Data.DataInterfaces;
using DiaryApp.Models.DTO;
using Microsoft.AspNetCore.Http;
using DiaryApp.Core.Entities;

namespace DiaryApp.API.Controllers
{
    public class MainPageController : PageController<MainPageDto, MainPage>
    {
        public MainPageController(IMainPageService mainPageService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(mainPageService, mapper, loggerFactory)
        {
        }

        [HttpGet("importantThingsArea/{pageID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ImportantThingsAreaDto>> GetImportantThingsAreaAsync(int pageID)
        {
            var model = await GetPageArea<ImportantThingsArea, ImportantThingsAreaDto>(pageID);
            return model;
        }

        [HttpGet("importantEventsArea/{pageID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ImportantEventsAreaDto>> GetImportantEventsAreaAsync(int pageID)
        {
            return await GetPageArea<ImportantEventsArea, ImportantEventsAreaDto>(pageID);
        }
    }
}