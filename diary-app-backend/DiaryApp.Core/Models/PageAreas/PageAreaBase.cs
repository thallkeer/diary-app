using DiaryApp.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Models
{
    public abstract class PageAreaBase<TPage> : BaseEntity, IPageArea
        where TPage : PageBase
    {
        public PageAreaBase()
        {

        }

        public PageAreaBase(TPage page, string header, bool withInitialization)
        {
            Page = page;
            PageId = page?.Id ?? 0;
            Header = header;

            if (withInitialization)
                Initialize();
        }

        [Required]
        [MaxLength(100)]
        public string Header { get; set; } = string.Empty;
        [Required]
        public int PageId { get; set; }
        public virtual TPage Page { get; set; }

        protected abstract void Initialize();
    }
}
