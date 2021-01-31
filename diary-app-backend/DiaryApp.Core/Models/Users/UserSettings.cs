using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Models
{
    public class UserSettings : BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        public virtual AppUser User { get; set; }
        public virtual PageAreaTransferSettings PageAreaTransferSettings { get; set; }
    }
}
