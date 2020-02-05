using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core
{
    public abstract class PageAreaBase
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Header { get; set; } = string.Empty;
        [Required]
        public int PageID { get; set; }
        public virtual PageBase Page { get; set; }

        public PageAreaBase()
        {

        }

        public PageAreaBase(PageBase page, string header)
        {
            Page = page;
            Header = header;
        }

        public override string ToString()
        {
            return Header;
        }
    }
}
