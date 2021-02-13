using System;

namespace DiaryApp.Models.DTO.Notifications
{
    public class NotificationDto : BaseDto
    {
        public int UserId { get; set; }
        public UserDto User { get; set; }

        public DateTime NotificationDate { get; set; }

        public string Subject { get; set; }
    }
}
