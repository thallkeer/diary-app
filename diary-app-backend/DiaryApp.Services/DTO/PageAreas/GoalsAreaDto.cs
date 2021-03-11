using System.Collections.Generic;
using DiaryApp.Services.DTO.Lists;

namespace DiaryApp.Services.DTO
{
    public class GoalsAreaDto : PageAreaDto
    {
        public List<GoalsListDto> GoalLists { get; set; }
    }
}
