using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;

namespace DiaryApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : AppBaseController<EventsController>
    {
        private readonly IEventService eventService;

        public EventsController(IEventService eventService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(mapper,loggerFactory)
        {
            this.eventService = eventService;
        }

        [HttpGet("{pageID}")]
        public IActionResult GetByPageID(int pageID)
        {
            try
            {
                var eventList = eventService.GetByPageID(pageID);
                if (eventList == null)
                    return NotFound();
                var model = mapper.Map<EventListModel>(eventList);
                return Ok(model);
            }
            catch (Exception ex)
            {
                logger.LogErrorWithDate(ex);
                return BadRequest(ex.Message);
            }
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
            eventData.Date = eventData.Date.ToLocalTime();
            var newEvent = mapper.Map<EventItem>(eventData);
            await eventService.AddItem(newEvent, eventData.OwnerID);
            return Ok(newEvent.ID);
        }

        [HttpPut("updateEvent}")]
        public async Task<IActionResult> UpdateEvent([FromBody] EventModel eventModel)
        {
            var _event = mapper.Map<EventItem>(eventModel);
            await eventService.UpdateItem(_event);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEventList([FromBody] EventListModel eventListModel)
        {
            var _eventList = mapper.Map<EventList>(eventListModel);
            await eventService.Update(_eventList);
            return Ok();
        }

        [HttpDelete("deleteEvent/{eventID}")]
        public async Task DeleteEvent(int eventID)
        {
            await eventService.DeleteItem(eventID);
        }

        [HttpDelete("{eventListID}")]
        public async Task DeleteEventList(int eventListID)
        {
            await eventService.Delete(eventListID);
        }
    }
}