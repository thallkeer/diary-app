using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
