using DiaryApp.Core.Entities;
using DiaryApp.Models.DTO;

namespace DiaryApp.Data.DataInterfaces.Lists
{
    public interface IEventItemService : ICrudService<EventItemDto, EventItem>
    {
    }
}
