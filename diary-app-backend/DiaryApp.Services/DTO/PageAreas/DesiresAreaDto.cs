using System.Collections.Generic;

namespace DiaryApp.Services.DTO
{
    public class DesiresAreaDto : PageAreaDto
    {
        public List<DesireListDto> DesiresLists { get; set; }
    }
}
