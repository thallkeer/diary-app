using System.Threading.Tasks;
using DiaryApp.Core.Entities.PageAreas;
using DiaryApp.Core.Entities.Pages;
using DiaryApp.Services.DataInterfaces;
using DiaryApp.Services.DTO;
using DiaryApp.Services.DTO.PageAreas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiaryApp.API.Controllers.Pages
{
    public class MainPageController : PageController<MainPageDto, MainPage>
    {
        public MainPageController(IMainPageService mainPageService)
            : base(mainPageService)
        {
        }

        [HttpGet("importantThingsArea/{pageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ImportantThingsAreaDto>> GetImportantThingsAreaAsync(int pageId)
        {
            return await PageService.GetPageAreaOrThrowAsync<ImportantThingsArea, ImportantThingsAreaDto>(pageId);
        }

        [HttpGet("importantEventsArea/{pageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ImportantEventsAreaDto>> GetImportantEventsAreaAsync(int pageId)
        {
            return await PageService.GetPageAreaOrThrowAsync<ImportantEventsArea, ImportantEventsAreaDto>(pageId);
        }
    }
}