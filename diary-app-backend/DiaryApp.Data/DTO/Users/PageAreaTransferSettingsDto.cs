using DiaryApp.Core.Models;

namespace DiaryApp.Data.DTO
{
    public class PageAreaTransferSettingsDto : BaseEntity
    {
        public int UserSettingsId { get; set; }
        public int UserId { get; set; }
        public bool TransferPurchasesArea { get; set; }
        public bool TransferDesiresArea { get; set; }
        public bool TransferIdeasArea { get; set; }
        public bool TransferGoalsArea { get; set; }        
    }
}