using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiaryApp.API.Models;
using DiaryApp.Core;
using DiaryApp.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiaryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ApplicationContext context;

        public EventsController(ApplicationContext context)
        {}

        [HttpGet("{month:int}/title/{title}")]
        public ActionResult<EventListModel> Get(int month, string title = "")
        {
            EventList eventList = context.EventLists.FirstOrDefault(ev => ev.Month == month && ev.Title == title);
            var model = new EventListModel
            {
                ID = eventList.ID,
                Month = eventList.Month,
                Title = eventList.Title,
                Items = new List<EventModel>()
            };

            foreach (EventItem eventModel in eventList.Items.OrderBy(e => e.Date))
            {
                model.Items.Add(new EventModel
                {
                    ID = eventModel.ID,
                    //Date = eventModel.Date.ToString("yyyy-MM-ddTHH:mm:ss"),
                    Date = eventModel.Date/*.ToString("yyyy-MM-dd")*/,
                    Subject = eventModel.Subject,
                    OwnerID = eventList.ID
                });
            }
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody]EventModel eventData)
        {
            var eventList = await context.EventLists.FindAsync(eventData.OwnerID);

            var newEvent = new EventItem
            {
                Subject = eventData.Subject,
                Date = eventData.Date,
                OwnerID = eventData.OwnerID
            };

            eventList.Items.Add(newEvent);            

            int saved = await context.SaveChangesAsync();

            if (saved != 0)
            {
                eventData.ID = newEvent.ID;
                return Ok(eventData);
            }
            return BadRequest();
        }

        [HttpDelete("{eventID}")]
        public async Task DeleteEvent(int eventID)
        {
            var eventToDelete = await context.Events.FindAsync(eventID);
            if (eventToDelete != null)
            {
                context.Events.Remove(eventToDelete);
                await context.SaveChangesAsync();
            }
        }
    }
}