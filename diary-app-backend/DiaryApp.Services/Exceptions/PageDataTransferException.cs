using System;

namespace DiaryApp.Services.Exceptions
{
    public class PageDataTransferException : Exception
    {
        public PageDataTransferException()
        {

        }
        public PageDataTransferException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
