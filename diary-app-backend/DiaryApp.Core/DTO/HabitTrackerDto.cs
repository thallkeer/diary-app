using System.Collections.Generic;

namespace DiaryApp.Core.DTO
{
    public class HabitTrackerDto : BaseDto
    {
        public string GoalName { get; set; }
        public List<HabitDayDto> SelectedDays { get; set; } = new List<HabitDayDto>();
        public int GoalsAreaID { get; set; }
    }
}
