using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core
{
    public class AppUser
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        public string ProfileImageUrl { get; set; }

        public override string ToString()
        {
            return $"{ID} {Username}";
        }
    }
}
