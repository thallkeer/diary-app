using DiaryApp.Services.DTO;

namespace DiaryApp.API.Models.Users
{
    public class UserWithSettingsResponse
    {
        public UserWithSettingsResponse(UserDto user, UserSettingsDto settings)
        {
            User = user;
            Settings = settings;
        }

        public UserDto User { get; set; }
        public UserSettingsDto Settings { get; set; }
    }
}
