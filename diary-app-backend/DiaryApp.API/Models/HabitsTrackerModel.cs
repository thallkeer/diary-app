using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiaryApp.API.Models
{
    public class HabitsTrackerModel
    {
        public int ID { get; set; }
        public string GoalName { get; set; }
        public List<int> SelectedDays { get; set; } = new List<int>();
        public int GoalsAreaID { get; set; }
    }
}
