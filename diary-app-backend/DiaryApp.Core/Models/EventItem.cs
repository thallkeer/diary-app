using System;

namespace DiaryApp.Core
{
    public class EventItem : ListItemBase<EventList>
    {
        public DateTime Date { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Url { get; set; }
        public bool FullDay { get; set; }
    }
}
