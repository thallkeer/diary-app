using System.Collections.Generic;
using DiaryApp.Services.DTO.Lists;

namespace DiaryApp.Services.DTO.PageAreas
{
    public class PurchasesAreaDto : PageAreaDto
    {
        public List<TodoListDto> PurchasesLists { get; set; }
    }
}
