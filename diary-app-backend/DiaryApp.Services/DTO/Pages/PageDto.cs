using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Services.DTO
{
    public class PageDto : BaseDto
    {
        public PageDto()
        {

        }

        public PageDto(int userId, int year, int month)
        {
            UserId = userId;
            Year = year;
            Month = month;
        }

        [Required]
        public int Year { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
