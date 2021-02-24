namespace DiaryApp.Services.DTO
{
    public class UserWithSettingsDto : UserDto
    {
        public UserSettingsDto Settings { get; set; }
    }
}
