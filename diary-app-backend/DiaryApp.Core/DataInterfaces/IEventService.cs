using System.Threading.Tasks;

namespace DiaryApp.Core
{
    public interface IEventService : ICrudService<EventList>, IListService<EventList, EventItem>
    {
        EventList GetByPageID(int pageID);
    }
}
