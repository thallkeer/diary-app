namespace DiaryApp.Services.Exceptions
{
    public class PageAlreadyExistsException : ApiException
    {
        public PageAlreadyExistsException() : base(System.Net.HttpStatusCode.BadRequest, new { Page = "Page with such parameters already exists" })
        {
        }
    }
}
