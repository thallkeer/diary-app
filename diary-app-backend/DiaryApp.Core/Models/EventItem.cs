using System;

namespace DiaryApp.Core
{
    public class EventItem : ListItemBase<EventList>
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }       
    }
}
