using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Services.DTO
{
    public class UserDto : BaseDto
    {
        [Required]
        public string Username { get; set; }
        public long? TelegramId { get; set; } 

        public UserDto(string userName)
        {
            Username = userName;
        }

        public UserDto()
        {

        }
    }
}
