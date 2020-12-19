using AutoMapper;
using Microsoft.Extensions.Logging;
using DiaryApp.Core.DTO;
using DiaryApp.Data.ServiceInterfaces;
using DiaryApp.Core.Models;

namespace DiaryApp.API.Controllers
{
    public class EventListsController : CrudController<EventListDto, EventList>
    {
        public EventListsController(IEventListService eventListService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(eventListService, mapper, loggerFactory)
        {
        }
    }
}