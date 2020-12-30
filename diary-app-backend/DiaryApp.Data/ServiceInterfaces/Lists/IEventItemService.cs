using DiaryApp.Data.DTO;
using DiaryApp.Core.Models;

namespace DiaryApp.Data.ServiceInterfaces.Lists
{
    public interface IEventItemService : ICrudService<EventItemDto, EventItem>
    {
    }
}
