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

    public class HabitsTracker
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required]
        [MaxLength(100)]
        public string GoalName { get; set; }
        public List<HabitDay> SelectedDays { get; set; } = new List<HabitDay>();
        [Required]
        public int GoalsAreaID { get; set; }
        public virtual GoalsArea GoalsArea { get; set; }

        public HabitsTracker() { }

        public HabitsTracker(HabitsTracker original, GoalsArea goalsArea)
        {
            this.GoalName = original.GoalName;
            this.SelectedDays = new List<HabitDay>(original.SelectedDays);
            original.SelectedDays.ForEach(sd => this.SelectedDays.Add(sd));            
            this.GoalsArea = goalsArea;
        }
    }
}
