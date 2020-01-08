using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using Microsoft.AspNetCore.Mvc;
using DiaryApp.Data.Services;
using System.Linq;

namespace DiaryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService eventService;
        private readonly IMapper mapper;

        public EventsController(ApplicationContext context, IMapper mapper)
        {
            this.eventService = new EventService(context);
            this.mapper = mapper;
        }

        [HttpGet("{pageID}")]
        public IActionResult GetByPageID(int pageID)
        {
            var eventList = eventService.GetByPageID(pageID);
            if (eventList == null)
                return NotFound();
            var model = mapper.Map<EventListModel>(eventList);
            return Ok(model);
        }

        [HttpGet("all/{pageID}")]
        public IActionResult GetAllByPageID(int pageID)
        {
            var lists = eventService.GetListsByPageID(pageID);
            var allLists = lists.Select(l => mapper.Map<EventListModel>(l));
            return Ok(allLists);
        }
 
        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody]EventModel eventData)
        {
            var newEvent = mapper.Map<EventItem>(eventData);
            await eventService.AddItem(newEvent, eventData.OwnerID);
            eventData.ID = newEvent.ID;
            return Ok(eventData);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTodo([FromBody] EventModel eventModel)
        {
            var _event = mapper.Map<EventItem>(eventModel);
            await eventService.UpdateItem(_event);
            return Ok();
        }

        [HttpDelete("{eventID}")]
        public async Task DeleteEvent(int eventID)
        {
            await eventService.DeleteItem(eventID);
        }
    }
}