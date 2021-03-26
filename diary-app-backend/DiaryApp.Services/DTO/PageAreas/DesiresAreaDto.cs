using System.Collections.Generic;
using DiaryApp.Services.DTO.Lists;

namespace DiaryApp.Services.DTO.PageAreas
{
    public class DesiresAreaDto : PageAreaDto
    {
        public List<CommonListDto> DesiresLists { get; set; }
    }
}
