﻿namespace DiaryApp.Models.DTO
{
    public class UserDto : BaseDto
    {
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
