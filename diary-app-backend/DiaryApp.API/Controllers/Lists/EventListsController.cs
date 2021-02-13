using AutoMapper;
using Microsoft.Extensions.Logging;
using DiaryApp.Models.DTO;
using DiaryApp.Services.DataInterfaces;
using DiaryApp.Core.Entities;

namespace DiaryApp.API.Controllers
{
    public class EventListsController : CrudController<EventListDto, EventList>
    {
        public EventListsController(ICrudService<EventListDto, EventList> eventListService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(eventListService, mapper, loggerFactory)
        {
        }
    }
}