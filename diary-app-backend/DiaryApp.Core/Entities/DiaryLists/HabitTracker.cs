using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DiaryApp.Core.Entities.PageAreas;

namespace DiaryApp.Core.Entities.DiaryLists
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
        public virtual List<HabitDay> SelectedDays { get; set; } = new();

        [Required]
        public int GoalsAreaId { get; set; }

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
