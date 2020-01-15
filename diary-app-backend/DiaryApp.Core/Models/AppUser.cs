using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core
{
    public class AppUser
    {
        [ScaffoldColumn(false)]
        public string ID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
