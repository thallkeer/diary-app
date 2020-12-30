
using DiaryApp.Data.DTO;
using DiaryApp.Core.Models;

namespace DiaryApp.Data.ServiceInterfaces
{
    public interface IEventListService : ICrudService<EventListDto, EventList>
    {
    }
}
