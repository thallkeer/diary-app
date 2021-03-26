using System.Collections.Generic;

namespace DiaryApp.Services.DTO.Lists
{
    public class HabitTrackerDto : BaseDto
    {
        public string GoalName { get; set; }
        public int GoalsAreaId { get; set; }
        public List<HabitDayDto> Items { get; set; } = new();
    }
}
