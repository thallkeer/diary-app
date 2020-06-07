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

        public PageAreaBase(PageBase page, string header, bool needInitialize)
        {
            Page = page;
            Header = header;
            if (needInitialize)
                Initialize();
        }

        /// <summary>
        /// Derived classes will override this method to copy their data to the desired year and month.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public abstract PageAreaBase TransferAreaData(PageBase page);

        /// <summary>
        /// Copy content from other area
        /// </summary>
        /// <param name="otherArea"></param>
        public abstract void AddFromOtherArea(PageAreaBase otherArea);

        /// <summary>
        /// Custom additional initialization
        /// </summary>
        protected abstract void Initialize();

        public override string ToString()
        {
            return Header;
        }
    }
}
