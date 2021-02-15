using DiaryApp.Core.Entities;
using DiaryApp.Services.DTO;

namespace DiaryApp.Services.DataInterfaces.Lists
{
    public interface IEventItemService : ICrudService<EventItemDto, EventItem>
    {
    }
}
