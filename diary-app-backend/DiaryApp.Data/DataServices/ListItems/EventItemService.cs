using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Models.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Data.DataInterfaces.Lists;
using System.Threading.Tasks;

namespace DiaryApp.Data.Services
{
    public class EventItemService : CrudService<EventItemDto, EventItem>, IEventItemService
    {
        public EventItemService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async override Task<int> CreateAsync(EventItemDto dto)
        {
            dto.Date = dto.Date.ToLocalTime();
            var eventId = await base.CreateAsync(dto);
            return eventId;
        }
    }
}
