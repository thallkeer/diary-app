using Microsoft.AspNetCore.Identity;

namespace DiaryApp.Core
{
    public class AppUser : IdentityUser
    {
        public string ProfileImageUrl { get; set; }

    }
}
