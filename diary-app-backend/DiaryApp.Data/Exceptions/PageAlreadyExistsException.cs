using DiaryApp.Core.Entities;
using System;

namespace DiaryApp.Data.Exceptions
{
    public class PageAlreadyExistsException : Exception
    {
        public PageAlreadyExistsException() : base("Page with such parameters already exists")
        {
        }
    }
}
