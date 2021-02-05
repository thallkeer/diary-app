using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Models.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Data.DataInterfaces;

namespace DiaryApp.Data.Services
{
    public class EventListService : CrudService<EventListDto, EventList>, IEventListService
    {
        public EventListService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
