using System.ComponentModel.DataAnnotations;

namespace DiaryApp.API.Models
{
    public class PageRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [Range(2020, 9999)]
        public int Year { get; set; }

        [Required]
        [Range(1, 12)]
        public int Month { get; set; }

        public override string ToString()
        {
            return $"{Year} {Month}, User: {UserId}";
        }
    }
}
