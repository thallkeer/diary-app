using System.Collections.Generic;

namespace DiaryApp.Models.DTO
{
    public class GoalsAreaDto : PageAreaDto
    {
        public List<HabitTrackerDto> GoalLists { get; set; }
    }
}
