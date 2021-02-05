namespace DiaryApp.Models.DTO
{
    public class UserWithPasswordDto : UserDto
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
