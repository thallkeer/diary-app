using System.Collections.Generic;

namespace DiaryApp.Data.DTO
{
    public class GoalsAreaDto : PageAreaDto
    {
        public List<HabitTrackerDto> GoalLists { get; set; }
    }
}
