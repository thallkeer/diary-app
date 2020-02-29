using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core
{
    public class HabitsTracker
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required]
        [MaxLength(100)]
        public string GoalName { get; set; }
        public List<int> SelectedDays { get; set; } = new List<int>();
        [Required]
        public int GoalsAreaID { get; set; }
        public virtual GoalsArea GoalsArea { get; set; }

        public HabitsTracker() { }

        public HabitsTracker(HabitsTracker original)
        {
            this.ID = 0;
            this.GoalName = original.GoalName;
            this.SelectedDays = new List<int>(original.SelectedDays);
            this.GoalsAreaID = original.GoalsAreaID;
        }
    }
}
