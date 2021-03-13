using System;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Entities
{
    public class EventItem : DiaryListItem
    {
        public EventItem() : base()
        {}

        public EventItem(EventItem original) : base(original)
        {
            Date = original.Date;
        }

        /// <summary>
        /// Date of event
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Location of event
        /// </summary>
        public string Location { get; set; }

        public virtual EventList Owner { get; set; }

        public override DiaryListItem GetCopy() => new EventItem(this);
    }
}
