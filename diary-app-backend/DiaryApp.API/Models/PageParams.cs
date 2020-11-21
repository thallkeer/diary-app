using System.ComponentModel.DataAnnotations;

namespace DiaryApp.API.Models
{
    public class PageParams
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int Month { get; set; }
    }
}
