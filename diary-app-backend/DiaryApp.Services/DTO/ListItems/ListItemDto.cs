using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Services.DTO
{
    public class ListItemDto : BaseDto
    {
        public string Url { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public int OwnerID { get; set; }
    }
}
