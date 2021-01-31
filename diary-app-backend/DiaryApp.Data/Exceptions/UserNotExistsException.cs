using System;

namespace DiaryApp.Data.Exceptions
{
    public class UserNotExistsException : Exception
    {
        public UserNotExistsException() : base("User with such id is not exists!")
        {}
    }
}
