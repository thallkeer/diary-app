using System;

namespace DiaryApp.Services.Exceptions
{
    public class PageDataTransferException : ApiException
    {
        public PageDataTransferException(Exception innerException) : base(System.Net.HttpStatusCode.InternalServerError, new { 
            Message = "Could not transfer page data to the next month, exception is occured.",
            Exception = innerException
        })
        {
        }
    }
}
