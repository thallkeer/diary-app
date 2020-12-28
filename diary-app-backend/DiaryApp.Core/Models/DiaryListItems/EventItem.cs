using DiaryApp.Core.Interfaces;
using System;
using System.Collections.Generic;

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

        public override bool Equals(object obj)
        {
            return obj is EventItem item &&
                   base.Equals(obj) &&
                   Date == item.Date &&
                   EqualityComparer<EventList>.Default.Equals(Owner, item.Owner);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Date, Owner);
        }
    }
}
