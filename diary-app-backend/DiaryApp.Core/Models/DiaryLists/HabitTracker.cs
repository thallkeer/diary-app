using DiaryApp.Core.Models;
using DiaryApp.Core.Models.PageAreas;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DiaryApp.Core
{
    /// <summary>
    /// Represents one day of month in a habit tracker
    /// </summary>
    public class HabitDay : BaseEntity
    {
        /// <summary>
        /// Number of day in month
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Note for day
        /// </summary>
        public string Note { get; set; }

        [Required]
        /// <summary>
        /// Id of habit tracker which this day is belongs to
        /// </summary>
        public int HabitTrackerId { get; set; }

        /// <summary>
        /// Habit tracker which this day is belongs to
        /// </summary>
        public virtual HabitTracker HabitTracker { get; set; }

        public HabitDay GetCopy()
        {
            return new HabitDay
            {
                Number = Number,
                Note = Note
            };
        }

        public override string ToString()
        {
            return $"{Number},{Note}";
        }
    }

    /// <summary>
    /// Tracks user habit along month
    /// </summary>
    public class HabitTracker : BaseEntity
    {
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

        public HabitTracker() { }

        public HabitTracker(string goalName)
        {
            GoalName = goalName;
        }

        public HabitTracker GetCopy()
        {
            var tracker = new HabitTracker(GoalName);
            var selectedDaysCopy = SelectedDays.Select(sd => sd.GetCopy());
            tracker.SelectedDays = new List<HabitDay>(selectedDaysCopy);
            return tracker;
        }
    }
}
