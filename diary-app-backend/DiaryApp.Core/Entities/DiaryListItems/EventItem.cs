using System;

namespace DiaryApp.Core.Entities
{
    public class EventItem : DiaryListItem
    {
        public EventItem() : base()
        {}

        public EventItem(EventItem original) : base(original)
        {
            this.Date = original.Date;
        }

        /// <summary>
        /// Date of event
        /// </summary>
        public DateTime Date { get; set; }

        public virtual new EventList Owner { get; set; }

        public override DiaryListItem GetCopy() => new EventItem(this);
    }
}
