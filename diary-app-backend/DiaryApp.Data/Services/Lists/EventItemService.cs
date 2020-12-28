using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Data.ServiceInterfaces.Lists;

namespace DiaryApp.Data.Services.Lists
{
    public class EventItemService : CrudService<EventItemDto, EventItem>, IEventItemService
    {
        public EventItemService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
