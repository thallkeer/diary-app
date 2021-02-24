using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Services.DTO.Users
{
    public class NotificationsSettingsDto : BaseDto
    {
        [Required]
        public int UserSettingsId { get; set; }
        public bool IsActivated { get; set; }
    }
}
