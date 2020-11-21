using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using DiaryApp.Core.Models;

namespace DiaryApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : AppBaseController<EventsController>
    {
        private readonly IEventService eventService;
        private readonly ListCrudContoller<EventList, EventItem, EventListModel, EventModel> crudController;

        public EventsController(IEventService eventService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(mapper,loggerFactory)
        {
            this.crudController = new ListCrudContoller<EventList, EventItem, EventListModel, EventModel>(eventService, mapper, logger);
            this.eventService = (IEventService) crudController.ListItemService;
        }

        [HttpPost]
        public async Task<IActionResult> AddEventList([FromBody] EventListModel eventListModel)
        {
            return await crudController.AddList(eventListModel);
        }
 
        [HttpPost("addEvent")]
        public async Task<IActionResult> AddEvent([FromBody]EventModel eventModel)
        {
            eventModel.Date = eventModel.Date?.ToLocalTime();
            return await crudController.AddItem(eventModel);
        }

        [HttpPut("updateEvent")]
        public async Task<IActionResult> UpdateEvent([FromBody] EventModel eventModel)
        {
            return await crudController.UpdateItem(eventModel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEventList([FromBody] EventListModel eventListModel)
        {
            return await crudController.UpdateList(eventListModel);
        }

        [HttpDelete("deleteEvent/{eventID}")]
        public async Task DeleteEvent(int eventID)
        {
            await crudController.DeleteItem(eventID);
        }

        [HttpDelete("{eventListID}")]
        public async Task DeleteEventList(int eventListID)
        {
            await crudController.DeleteList(eventListID);
        }
    }
}