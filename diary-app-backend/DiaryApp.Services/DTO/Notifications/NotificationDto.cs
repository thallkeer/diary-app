using System;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Services.DTO.Notifications
{
    public class NotificationDto : BaseDto
    {
        [Required]
        public int UserId { get; set; }
        public long UserTelegramId { get; set; }
        [Required]
        public int EventId { get; set; }
        [Required]
        public DateTime NotificationDate { get; set; }        
        public string Subject { get; set; }
    }
}
