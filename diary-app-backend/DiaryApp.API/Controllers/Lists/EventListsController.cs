using DiaryApp.Services.DataInterfaces;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DTO.Lists;

namespace DiaryApp.API.Controllers
{
    public class EventListsController : CrudController<EventListDto, EventList>
    {
        public EventListsController(ICrudService<EventListDto, EventList> eventListService)
            : base(eventListService)
        {
        }
    }
}