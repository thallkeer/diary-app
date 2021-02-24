using System.ComponentModel.DataAnnotations;

namespace DiaryApp.API.Requests
{
    public class UpdateNotificationSettingsRequest
    {
        public int Id { get; set; }
        [Required]
        public int UserSettingsId { get; set; }
        public bool IsActivated { get; set; }
    }
}
