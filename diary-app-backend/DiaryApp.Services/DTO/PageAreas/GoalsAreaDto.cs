using System.Collections.Generic;

namespace DiaryApp.Services.DTO
{
    public class GoalsAreaDto : PageAreaDto
    {
        public List<HabitTrackerDto> GoalLists { get; set; }
    }
}
