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

        /// <summary>
        /// Derived classes will override this method to copy their data to the desired year and month.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public abstract PageAreaBase TransferAreaData(PageBase page);

        public override string ToString()
        {
            return Header;
        }
    }
}
