using System.Collections.Generic;

namespace DiaryApp.Core.DTO
{
    public class PurchasesAreaDto : PageAreaDto
    {
        public List<TodoListDto> PurchasesLists { get; set; }
    }
}
