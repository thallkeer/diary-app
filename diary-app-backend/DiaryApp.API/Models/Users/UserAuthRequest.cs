using System.ComponentModel.DataAnnotations;

namespace DiaryApp.API.Models.Users
{
    public class UserAuthRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
