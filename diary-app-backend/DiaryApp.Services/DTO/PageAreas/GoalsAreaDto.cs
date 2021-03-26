using System.Collections.Generic;
using DiaryApp.Services.DTO.Lists;

namespace DiaryApp.Services.DTO.PageAreas
{
    public class GoalsAreaDto : PageAreaDto
    {
        public List<HabitTrackerDto> GoalLists { get; set; }
    }
}
