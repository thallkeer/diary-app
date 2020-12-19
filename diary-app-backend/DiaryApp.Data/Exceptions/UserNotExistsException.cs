using System;

namespace DiaryApp.Data.Exceptions
{
    public class UserNotExistsException : Exception
    {
        public UserNotExistsException(string message) : base(message)
        {
        }
    }
}
