namespace DiaryApp.Core.Models
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
