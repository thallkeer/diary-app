using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Services.DTO
{
    public class UserWithPasswordDto : UserDto
    {
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
    }
}
