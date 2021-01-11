using System.Collections.Generic;

namespace DiaryApp.Data.DTO
{
    public class PurchasesAreaDto : PageAreaDto
    {
        public List<PurchaseListDto> PurchasesLists { get; set; }
    }
}
