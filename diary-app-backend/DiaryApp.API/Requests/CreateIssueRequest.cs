using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Services.Requests
{
    public class CreateIssueRequest
    {
        public string UserName { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
