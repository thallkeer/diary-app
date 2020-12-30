using System.Collections.Generic;

namespace DiaryApp.Data.DTO
{
    public class PurchasesAreaDto : PageAreaDto
    {
        public List<TodoListDto> PurchasesLists { get; set; }
    }
}
