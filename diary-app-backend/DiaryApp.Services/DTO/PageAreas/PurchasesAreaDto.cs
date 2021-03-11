using System.Collections.Generic;

namespace DiaryApp.Services.DTO
{
    public class PurchasesAreaDto : PageAreaDto
    {
        public List<PurchasesListDto> PurchasesLists { get; set; }
    }
}
