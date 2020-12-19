using DiaryApp.Core.Models;
using DiaryApp.Core.Models.PageAreas;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core
{
    /// <summary>
    /// Represents one day of month in a habit tracker
    /// </summary>
    public class HabitDay
    {
        /// <summary>
        /// Number of day in month
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Note for day
        /// </summary>
        public string Note { get; set; }

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
        /// Days of habit. Contains only days user marked.
        /// </summary>
        public List<HabitDay> SelectedDays { get; set; } = new List<HabitDay>();

        [Required]
        public int GoalsAreaID { get; set; }

        public virtual GoalsArea GoalsArea { get; set; }

        public HabitTracker() { }

        public HabitTracker(HabitTracker original, GoalsArea goalsArea)
        {
            this.GoalName = original.GoalName;
            this.SelectedDays = new List<HabitDay>(original.SelectedDays);
            //TODO: try to harmonize with the rest of the lists
            original.SelectedDays.ForEach(sd => this.SelectedDays.Add(sd));
            this.GoalsArea = goalsArea;
        }
    }
}
