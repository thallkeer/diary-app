using System.Collections.Generic;

namespace DiaryApp.Models.DTO
{
    public class PurchasesAreaDto : PageAreaDto
    {
        public List<PurchaseListDto> PurchasesLists { get; set; }
    }
}
