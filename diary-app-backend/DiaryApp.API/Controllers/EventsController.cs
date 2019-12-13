using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiaryApp.API.Models;
using DiaryApp.Core;
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
                context.AddRange(new List<EventModel>
                {
                    new EventModel
                    {
                        Subject = "Покормить котика",
                        Start = new DateTime(2019,12,5),
                        Description = "Необходимо покормить котика"
                         
                    },
                     new EventModel
                    {
                        Subject = "Сходить в сервисный центр",
                        Start = new DateTime(2019,12,11),
                        Description = ""
                    },
                      new EventModel
                    {
                        Subject = "В рестик тусить",
                        Start = new DateTime(2019,12,20),
                        Description = ""
                      }
                });

                context.SaveChanges();
            }
        }

        [HttpGet("{month}")]
        public ActionResult<List<EventViewModelLight>> Get(int month)
        {
            var events = context.Events.Where(ev => ev.Start.Month == month);
            List<EventViewModelLight> model = new List<EventViewModelLight>();
            foreach (EventModel eventModel in events.OrderBy(e => e.Start))
            {
                model.Add(new EventViewModelLight
                {
                    EventID = eventModel.ID,
                    //Date = eventModel.Start.ToString("yyyy-MM-ddTHH:mm:ss"),
                    Date = eventModel.Start.ToString("yyyy-MM-dd"),
                    Subject = eventModel.Subject,
                    FullDay = false
                });
            }
            return model;
        }                 

        [HttpPost("{month:int}/day/{day:int}")]       
        public async Task<IActionResult> AddEvent(int month, int day,[FromBody]EventViewModelLight eventData)
        {
            await context.Events.AddAsync(new EventModel
            {
                Subject = eventData.Subject,
                Start = DateTime.Parse(eventData.Date),
                FullDay = eventData.FullDay
            });

            int saved = await context.SaveChangesAsync();

            if (saved != 0)
                return Ok();
            return BadRequest();
        }
    }
}