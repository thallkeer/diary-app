using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Entities.Users.Settings
{
    public class UserSettings : BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        public virtual AppUser User { get; set; }
        public virtual PageAreaTransferSettings PageAreaTransferSettings { get; set; }
        public virtual NotificationSettings NotificationSettings { get; set; }
    }
}
