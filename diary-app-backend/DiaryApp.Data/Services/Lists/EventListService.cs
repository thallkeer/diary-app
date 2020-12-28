using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Data.ServiceInterfaces;

namespace DiaryApp.Data.Services
{
    public class EventListService : CrudService<EventListDto, EventList>, IEventListService
    {
        public EventListService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
