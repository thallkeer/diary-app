using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Services.DTO
{
    public class PageAreaTransferSettingsDto : BaseDto
    {
        [Required]
        public int UserSettingsId { get; set; }
        [Required]
        public int UserId { get; set; }
        public bool TransferPurchasesArea { get; set; }
        public bool TransferDesiresArea { get; set; }
        public bool TransferIdeasArea { get; set; }
        public bool TransferGoalsArea { get; set; }        
    }
}