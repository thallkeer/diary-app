using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core
{
    public abstract class PageAreaBase
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Header { get; set; }
        [Required]
        public int PageID { get; set; }
        public virtual PageBase Page { get; set; }
    }
}
