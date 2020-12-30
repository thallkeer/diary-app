using AutoMapper;
using DiaryApp.Data.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Data.ServiceInterfaces.Lists;
using Microsoft.Extensions.Logging;

namespace DiaryApp.API.Controllers.Lists
{
    public class EventsController : CrudController<EventItemDto, EventItem>
    {
        public EventsController(IEventItemService eventItemService, IMapper mapper, ILoggerFactory loggerFactory) : base(eventItemService, mapper, loggerFactory)
        {
        }
    }
}
