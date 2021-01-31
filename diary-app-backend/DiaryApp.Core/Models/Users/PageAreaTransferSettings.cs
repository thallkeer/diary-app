using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Models
{
    /// <summary>
    /// Represents settings for automatically transferring page areas data when next month page is created.
    /// </summary>
    public class PageAreaTransferSettings : BaseEntity
    {
        [Required]
        public int UserSettingsId { get; set; }
        public virtual UserSettings UserSettings { get; set; }
        public bool TransferPurchasesArea { get; set; }
        public bool TransferDesiresArea { get; set; }        
        public bool TransferIdeasArea { get; set; }
        public bool TransferGoalsArea { get; set; }
    }
}
