﻿using System.Collections.Generic;

namespace DiaryApp.Services.DTO
{
    public class PurchasesAreaDto : PageAreaDto
    {
        public List<PurchaseListDto> PurchasesLists { get; set; }
    }
}