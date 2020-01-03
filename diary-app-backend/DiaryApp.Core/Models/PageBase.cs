using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core
{
    public abstract class PageBase
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int Month { get; set; }
        public virtual AppUser User { get; set; }
    }
}
