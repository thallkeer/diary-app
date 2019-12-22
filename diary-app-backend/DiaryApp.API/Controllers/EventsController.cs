using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiaryApp.API.Models;
using DiaryApp.Core;
using DiaryApp.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiaryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ApplicationContext context;

        public EventsController(ApplicationContext context)
        {
            this.context = context;            
            if (!context.Events.Any())
            {
                var events = new List<EventItem>
                {
                    new EventItem
                    {
                        Subject = "Покормить котика",
                        Start = new DateTime(2019,12,5),
                        Description = "Необходимо покормить котика"                        
                    },
                     new EventItem
                    {
                        Subject = "Сходить в сервисный центр",
                        Start = new DateTime(2019,12,11),
                        Description = ""
                    },
                      new EventItem
                    {
                        Subject = "В рестик тусить",
                        Start = new DateTime(2019,12,20),
                        Description = ""
                      }
                };

                var eventList = new EventList() { Items = events, Title = "Важные события", PageID = 1, Month = 12 };
                events.ForEach(e => e.Owner = eventList);

                context.Events.Add(eventList);
                context.SaveChanges();
            }
        }

        [HttpGet("{month:int}/title/{title}")]
        public ActionResult<EventListViewModel> Get(int month, string title = "")
        {
            EventList eventList = context.Events.FirstOrDefault(ev => ev.Month == month && ev.Title == title);
            EventListViewModel model = new EventListViewModel {
                ID = eventList.ID,
                Month = eventList.Month,
                PageID = eventList.PageID,
                Title = eventList.Title
            };
            foreach (EventItem eventModel in eventList.Items.OrderBy(e => e.Start))
            {
                model.Items.Add(new EventViewModelLight
                {
                    EventID = eventModel.ID,
                    //Date = eventModel.Start.ToString("yyyy-MM-ddTHH:mm:ss"),
                    Date = eventModel.Start.ToString("yyyy-MM-dd"),
                    Subject = eventModel.Subject,                    
                    FullDay = false,
                    OwnerID = eventList.ID
                });
            }
            return model;
        }                 

        //[HttpPost("{month:int}/day/{day:int}")]       
        //public async Task<IActionResult> AddEvent(int month, int day,[FromBody]EventViewModelLight eventData)
        //{
        //    await context.Events.AddAsync(new EventItem
        //    {
        //        Subject = eventData.Subject,
        //        Start = DateTime.Parse(eventData.Date),
        //        FullDay = eventData.FullDay
        //    });

        //    int saved = await context.SaveChangesAsync();

        //    if (saved != 0)
        //        return Ok();
        //    return BadRequest();
        //}

        [HttpDelete("{eventID}")]
        public async Task DeleteEvent(int eventID)
        {
            var eventToDelete = context.Events.FirstOrDefault(ev => ev.ID == eventID);
            if (eventToDelete != null)
            {
                context.Events.Remove(eventToDelete);
                await context.SaveChangesAsync();
            }
        }

        [HttpGet("/calendar/{month}")]
        public async Task GetCalendar(int month)
        {
            var year = DateTime.Now.Year;
            int daysInMonth = DateTime.DaysInMonth(year, month);
            var firstDayOfMonth = new DateTime(year, month, 1);
        }
    }
}