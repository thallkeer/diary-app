using AutoMapper;
using DiaryApp.Core.DTO;
using DiaryApp.Data.ServiceInterfaces.Lists;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiaryApp.API.Controllers.Lists
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : AppBaseController<EventsController>
    {
        private readonly IEventItemService _eventItemService;
        public EventsController(IEventItemService eventItemService, IMapper mapper, ILoggerFactory loggerFactory) : base(mapper, loggerFactory)
        {
            _eventItemService = eventItemService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] EventItemDto eventModel)
        {
            var id = await _eventItemService.CreateAsync(eventModel);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] EventItemDto eventModel)
        {
            await _eventItemService.UpdateAsync(eventModel);
            return Ok();
        }

        [HttpDelete("{eventID}")]
        public async Task<IActionResult> DeleteAsync(int eventID)
        {
            await _eventItemService.DeleteAsync(eventID);
            return NoContent();
        }
    }
}
