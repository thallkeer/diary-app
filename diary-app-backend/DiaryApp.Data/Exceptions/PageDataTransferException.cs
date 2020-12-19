using System;

namespace DiaryApp.Data.Exceptions
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
