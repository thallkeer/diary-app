using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using Microsoft.AspNetCore.Mvc;
using DiaryApp.Data.Services;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace DiaryApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService eventService;
        private readonly IMapper mapper;

        public EventsController(IEventService eventService, IMapper mapper)
        {
            this.eventService = eventService;
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
        public async Task<IActionResult> AddEventList([FromBody] EventListModel eventListModel)
        {
            var eventList = mapper.Map<EventList>(eventListModel);
            await eventService.Create(eventList);
            return Ok(eventList.ID);
        }
 
        [HttpPost("addEvent")]
        public async Task<IActionResult> AddEvent([FromBody]EventModel eventData)
        {
            var newEvent = mapper.Map<EventItem>(eventData);
            await eventService.AddItem(newEvent, eventData.OwnerID);
            return Ok(newEvent.ID);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvent([FromBody] EventModel eventModel)
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