using System.ComponentModel.DataAnnotations;

namespace DiaryApp.API.Requests
{
    public class UpdateTransferSettingsRequest
    {
        public int Id { get; set; }
        [Required]
        public int UserSettingsId { get; set; }
        public bool TransferPurchasesArea { get; set; }
        public bool TransferDesiresArea { get; set; }
        public bool TransferIdeasArea { get; set; }
        public bool TransferGoalsArea { get; set; }
    }
}
