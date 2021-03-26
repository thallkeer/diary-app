using DiaryApp.API.Models;
using DiaryApp.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DiaryApp.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly NLog.ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
            _logger = NLog.LogManager.GetCurrentClassLogger();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            ErrorDetails errors = null;

            switch (ex)
            {
                case HttpException re:
                    _logger.Error(ex, "REST ERROR");
                    errors = new ErrorDetails()
                    {
                        StatusCode = (int)re.StatusCode,
                        Message = re.Message
                    };
                    break;
                case Exception e:
                    _logger.Error(ex, "SERVER ERROR");
                    errors = new ErrorDetails()
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Message = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message
                    };
                    break;
            }

            context.Response.ContentType = "application/json";            
            if (errors != null)
            {
                context.Response.StatusCode = errors.StatusCode;
                var result = JsonConvert.SerializeObject(new
                {
                    errors
                });

                await context.Response.WriteAsync(result);
            }
        }
    }
}
