using DiaryApp.Services.DTO.Users;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Services.DTO
{
    public class UserSettingsDto : BaseDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public PageAreaTransferSettingsDto PageAreaTransferSettings { get; set; }

        [Required]
        public NotificationsSettingsDto NotificationSettings { get; set; }
    }
}
