namespace DiaryApp.Core.Models
{
    public class EventList : DiaryList<EventItem>
    {
        public EventList()
        {

        }
        public EventList(string title) : base(title)
        {
        }
    }
}
