using DiaryApp.Core.Models;
using DiaryApp.Core.Models.PageAreas;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core
{
    public class HabitDay
    {
        public int Number { get; set; }
        public string Note { get; set; }

        public override string ToString()
        {
            return $"{Number},{Note}";
        }
    }

    public class HabitTracker : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string GoalName { get; set; }
        public List<HabitDay> SelectedDays { get; set; } = new List<HabitDay>();
        [Required]
        public int GoalsAreaID { get; set; }
        public virtual GoalsArea GoalsArea { get; set; }

        public HabitTracker() { }

        public HabitTracker(HabitTracker original, GoalsArea goalsArea)
        {
            this.GoalName = original.GoalName;
            this.SelectedDays = new List<HabitDay>(original.SelectedDays);
            original.SelectedDays.ForEach(sd => this.SelectedDays.Add(sd));            
            this.GoalsArea = goalsArea;
        }
    }
}
