using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Models.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Data.DataInterfaces.Lists;

namespace DiaryApp.Data.Services
{
    public class EventItemService : CrudService<EventItemDto, EventItem>, IEventItemService
    {
        public EventItemService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
