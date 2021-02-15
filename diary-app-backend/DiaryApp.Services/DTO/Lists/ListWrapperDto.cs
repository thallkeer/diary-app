using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Services.DTO
{
    public class ListWrapperDto : BaseDto
    {
        [Required]
        public int AreaOwnerId { get; set; }
    }
}
