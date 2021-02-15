using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Services.DTO
{
    public class PageAreaDto : BaseDto
    {
        public string Header { get; set; }
        [Required]
        public int PageId { get; set; }
    }
}
