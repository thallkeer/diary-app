using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DiaryApp.Core.Entities.Users;

namespace DiaryApp.Core.Entities.Notifications
{
    public class Notification : BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        public virtual AppUser User { get; set; }

        [Required]
        public int EventId { get; set; }
        public virtual EventItem Event { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime NotificationDate { get; set; }

        [Required]
        public string Subject { get; set; }

        public static string GetSubjectForEvent(EventItem eventItem, bool forDayBefore)
        {
            StringBuilder subject = new("Напоминание: ");
            subject.Append(forDayBefore ? "завтра" : "сегодня");
            subject.AppendLine($" {eventItem.Date.ToShortDateString()} в {eventItem.Date.ToShortTimeString()} состоится {eventItem.Subject}.");
            if (!string.IsNullOrEmpty(eventItem.Location))
                subject.Append($"Место: {eventItem.Location}.");
            return subject.ToString();
        }
    }
}
