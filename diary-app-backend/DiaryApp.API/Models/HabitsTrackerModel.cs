using DiaryApp.Core;
using System.Collections.Generic;

namespace DiaryApp.API.Models
{
    public class HabitsTrackerModel
    {
        public int ID { get; set; }
        public string GoalName { get; set; }
        public List<HabitDay> SelectedDays { get; set; } = new List<HabitDay>();
        public int GoalsAreaID { get; set; }
    }
}
