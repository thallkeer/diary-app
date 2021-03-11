using System.Collections.Generic;

namespace DiaryApp.Services.DTO
{
    public class DesiresAreaDto : PageAreaDto
    {
        public List<DesiresListDto> DesiresLists { get; set; }
    }
}
