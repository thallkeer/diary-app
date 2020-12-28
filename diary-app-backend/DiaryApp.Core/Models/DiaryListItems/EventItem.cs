using DiaryApp.Core.Interfaces;
using System;

namespace DiaryApp.Core.Models
{
    public class EventItem : ListItemBase, IDiaryListItem<EventList, EventItem>
    {
        /// <summary>
        /// Date of event
        /// </summary>
        public DateTime Date { get; set; }
        public virtual EventList Owner { get; set; }

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
