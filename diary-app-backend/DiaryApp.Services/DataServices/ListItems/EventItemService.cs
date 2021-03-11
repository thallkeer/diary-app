using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Services.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DataInterfaces.ListItems;
using System.Threading.Tasks;

namespace DiaryApp.Services.DataServices
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
