using DiaryApp.Core.Entities;
using DiaryApp.Services.DTO;

namespace DiaryApp.Services.DataInterfaces.ListItems
{
    public interface IEventItemService : ICrudService<EventItemDto, EventItem>
    {
    }
}
