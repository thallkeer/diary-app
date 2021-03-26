using System;
using System.Net;

namespace DiaryApp.Services.Exceptions
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public object Errors { get; }

        public HttpException(HttpStatusCode statusCode, object errors = null)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }
}
