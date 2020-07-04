using DiaryApp.Core;
using DiaryApp.Core.Models;

namespace DiaryApp.Data.Services
{
    public class EventService : ListService<EventList, EventItem>, IEventService
    {
        public EventService(ApplicationContext context) : base(context)
        {
        }
    }
}
