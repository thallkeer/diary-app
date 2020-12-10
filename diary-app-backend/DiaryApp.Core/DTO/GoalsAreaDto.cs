using System.Collections.Generic;

namespace DiaryApp.Core.DTO
{
    public class GoalsAreaDto : PageAreaDto
    {
        public List<HabitTrackerDto> GoalLists { get; set; }
    }
}
