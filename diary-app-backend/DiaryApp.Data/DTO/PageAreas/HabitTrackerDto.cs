using System.Collections.Generic;

namespace DiaryApp.Data.DTO
{
    public class HabitTrackerDto : BaseDto
    {
        public string GoalName { get; set; }
        public List<HabitDayDto> Items { get; set; } = new List<HabitDayDto>();
        public int GoalsAreaID { get; set; }
    }
}
