namespace DiaryApp.Core.DTO
{
    public class UserDto : BaseDto
    {
        public string Username { get; set; }

        public UserDto(string userName)
        {
            Username = userName;
        }

        public UserDto()
        {

        }
    }
}
