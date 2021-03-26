using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Services.DTO
{
    public class HabitDayDto : BaseDto
    {
        [Required]
        public int Number { get; set; }
        public string Note { get; set; }
        [Required]
        public int HabitTrackerId { get; set; }
    }
}
