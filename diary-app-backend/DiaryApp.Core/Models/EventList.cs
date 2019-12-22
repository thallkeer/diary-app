using System.Collections.Generic;

namespace DiaryApp.Core.Models
{
    public class EventList : IListModel<EventItem>
    {
        public int ID { get; set; }
        public int PageID { get; set; }
        public string Title { get; set; }
        public virtual List<EventItem> Items { get; set; } = new List<EventItem>();
        public int Month { get; set; }        
    }
}
