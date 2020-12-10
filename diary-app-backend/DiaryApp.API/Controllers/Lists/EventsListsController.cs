using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using DiaryApp.Core.DTO;
using DiaryApp.Data.ServiceInterfaces;

namespace DiaryApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventListsController : AppBaseController<EventListsController>
    {
        private readonly IEventListService _eventListService;

        public EventListsController(IEventListService eventListService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(mapper, loggerFactory)
        {
            _eventListService = eventListService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] EventListDto eventListModel)
        {
            var id = await _eventListService.CreateAsync(eventListModel);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] EventListDto eventListModel)
        {
             await _eventListService.UpdateAsync(eventListModel);
            return Ok();
        }

        [HttpDelete("{eventListID}")]
        public async Task<IActionResult> DeleteAsync(int eventListID)
        {
            await _eventListService.DeleteAsync(eventListID);
            return NoContent();
        }
    }
}