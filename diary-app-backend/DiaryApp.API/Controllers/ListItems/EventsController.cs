using AutoMapper;
using DiaryApp.Models.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DataInterfaces.Lists;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DiaryApp.API.Controllers.Lists
{
    public class EventsController : CrudController<EventItemDto, EventItem>
    {
        private readonly IEventItemService _eventItemService;

        public EventsController(IEventItemService eventItemService, IMapper mapper, ILoggerFactory loggerFactory) 
            : base(eventItemService, mapper, loggerFactory)
        {
            _eventItemService = eventItemService;
        }

        public async override Task<ActionResult<int>> PostAsync([FromBody] EventItemDto createModel)
        {
            var userIdString = User.Identity.Name;
            if (string.IsNullOrEmpty(userIdString))
                return await base.PostAsync(createModel);
            var id = await _eventItemService.CreateWithNotificationAsync(createModel, int.Parse(userIdString));
            return Ok(id);
        }
    }
}
