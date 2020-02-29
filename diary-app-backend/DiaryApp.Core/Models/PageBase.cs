using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core
{
    public abstract class PageBase
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required]
        [Range(2020,9999)]
        public int Year { get; set; }
        [Required]
        [Range(1, 12)]
        public int Month { get; set; }
        [Required]
        public int UserID { get; set; }
        public virtual AppUser User { get; set; }

        public PageBase()
        {

        }

        public PageBase(int year, int month, AppUser user)
        {
            Year = year;
            Month = month;
            User = user;
        }
    }
}
