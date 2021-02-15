using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Services.DTO
{
    public class TodoItemDto : ListItemDto
    {
        [Required]
        public bool Done { get; set; }
    }
}
