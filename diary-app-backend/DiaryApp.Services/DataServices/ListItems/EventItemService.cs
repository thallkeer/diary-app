using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Services.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DataInterfaces.Lists;
using System.Threading.Tasks;
using DiaryApp.Services.DataInterfaces;
using DiaryApp.Services.DTO.Notifications;
using DiaryApp.Core.Entities.Notifications;

namespace DiaryApp.Services.Services
{
    public class EventItemService : CrudService<EventItemDto, EventItem>, IEventItemService
    {
        public EventItemService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async override Task<int> CreateAsync(EventItemDto dto)
        {
            dto.Date = dto.Date.ToLocalTime();
            return await base.CreateAsync(dto);
        }
    }
}
