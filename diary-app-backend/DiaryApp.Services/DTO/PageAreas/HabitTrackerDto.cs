using System.Collections.Generic;

namespace DiaryApp.Services.DTO
{
    public class HabitTrackerDto : ListWrapperDto
    {
        public string GoalName { get; set; }
        public List<HabitDayDto> Items { get; set; } = new List<HabitDayDto>();
    }
}
