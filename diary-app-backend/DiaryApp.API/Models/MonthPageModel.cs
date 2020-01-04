using System.Collections.Generic;

namespace DiaryApp.API.Models
{
    public class MonthPageModel
    {
        public PurchasesAreaModel PurchasesArea { get; set; }
        public DesiresAreaModel DesiresArea { get; set; }
        public IdeasAreaModel IdeasArea { get; set; }
        public GoalsAreaModel GoalsArea { get; set; }
    }
}
