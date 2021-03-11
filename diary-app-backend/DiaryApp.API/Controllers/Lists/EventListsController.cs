using DiaryApp.Services.DTO;
using DiaryApp.Services.DataInterfaces;
using DiaryApp.Core.Entities;

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