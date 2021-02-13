using DiaryApp.Core.Entities.Users.Settings;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Entities
{
    public class AppUser : BaseEntity
    {
        /// <summary>
        /// Telegram user identifier to send notifications
        /// </summary>
        public long? TelegramId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        public virtual UserSettings Settings { get; set; }

        public override string ToString()
        {
            return $"{Id} {Username}";
        }
    }
}
