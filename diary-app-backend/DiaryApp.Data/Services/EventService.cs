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
    }
}
