using System.Threading.Tasks;
using System.Linq;
using DiaryApp.Core;

namespace DiaryApp.Data.Services
{
    public class EventService : ListService<EventList, EventItem>, IEventService
    {
        private readonly CrudService<EventItem> eventItemsService;
        public EventService(ApplicationContext context) : base(context)
        {
            eventItemsService = new CrudService<EventItem>(context);
        }

        public EventList GetByPageID(int pageID)
        {
            return dbSet.FirstOrDefault(el => el.PageID == pageID);
        }
    }
}
