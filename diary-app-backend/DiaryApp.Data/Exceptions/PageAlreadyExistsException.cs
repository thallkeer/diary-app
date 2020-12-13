using System;

namespace DiaryApp.Data.Exceptions
{
    public class PageAlreadyExistsException : Exception
    {
        public PageAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
