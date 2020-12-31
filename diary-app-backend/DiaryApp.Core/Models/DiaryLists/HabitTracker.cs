using DiaryApp.Core.Models.PageAreas;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DiaryApp.Core.Models
{
    /// <summary>
    /// Tracks user habit along month
    /// </summary>
    public class HabitTracker : BaseEntity
    {
        public HabitTracker() { }

        public HabitTracker(string goalName)
        {
            GoalName = goalName;
        }

        [Required]
        [MaxLength(100)]
        public string GoalName { get; set; }

        /// <summary>
        /// Days of habit. Contains only days user has marked.
        /// </summary>
        public virtual List<HabitDay> SelectedDays { get; set; } = new List<HabitDay>();

        [Required]
        public int GoalsAreaID { get; set; }

        public virtual GoalsArea GoalsArea { get; set; }        

        public HabitTracker GetCopy()
        {
            var tracker = new HabitTracker(GoalName);
            var selectedDaysCopy = SelectedDays.Select(sd => sd.GetCopy());
            tracker.SelectedDays = new List<HabitDay>(selectedDaysCopy);
            return tracker;
        }

        public override string ToString()
        {
            return $"{Id} {GoalName}";
        }
    }
}
