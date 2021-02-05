using AutoMapper;
using Microsoft.Extensions.Logging;
using DiaryApp.Models.DTO;
using DiaryApp.Data.DataInterfaces;
using DiaryApp.Core.Entities;

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