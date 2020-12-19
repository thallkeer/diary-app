using System;

namespace DiaryApp.Core.Models
{
    public class EventItem : ListItemBase
    {
        public DateTime Date { get; set; }
        public virtual new EventList Owner { get; set; }

        public EventItem()
        {

        }

        public EventItem(EventItem original) : base(original)
        {           
            this.Date = original.Date;
        }

        public override ListItemBase GetCopy() => new EventItem(this);
    }
}
