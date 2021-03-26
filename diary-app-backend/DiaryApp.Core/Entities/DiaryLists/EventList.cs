using DiaryApp.Core.Entities.DiaryLists;

namespace DiaryApp.Core.Entities
{
    public class EventList : DiaryList<EventItem>
    {
        public EventList() : base()
        {

        }
        public EventList(string title) : base(title)
        {
        }
    }
}
