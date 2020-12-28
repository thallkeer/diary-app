using DiaryApp.Core.Models.PageAreas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryApp.Core.Models
{
    public class WeekDay : BaseEntity
    {
        /// <summary>
        /// The exact date of the day
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime Day { get; set; }
        /// <summary>
        /// Day content
        /// </summary>
        public string Subject { get; set; }
        [Required]
        public int WeekPlansAreaID { get; set; }
        public virtual WeekPlansArea WeekPlansArea { get; set; }

        public override bool Equals(object obj)
        {
            return obj is WeekDay day &&
                   Id == day.Id &&
                   Day == day.Day &&
                   Subject == day.Subject &&
                   WeekPlansAreaID == day.WeekPlansAreaID &&
                   EqualityComparer<WeekPlansArea>.Default.Equals(WeekPlansArea, day.WeekPlansArea);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Day, Subject, WeekPlansAreaID, WeekPlansArea);
        }
    }
}
