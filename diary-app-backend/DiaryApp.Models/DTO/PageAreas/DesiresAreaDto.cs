using System.Collections.Generic;

namespace DiaryApp.Models.DTO
{
    public class DesiresAreaDto : PageAreaDto
    {
        public List<DesireListDto> DesiresLists { get; set; }
    }
}
