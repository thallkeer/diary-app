using Microsoft.AspNetCore.Identity;

namespace DiaryApp.Core.Models
{
    public class AppUser : IdentityUser
    {
        public string ProfileImageUrl { get; set; }

    }
}
