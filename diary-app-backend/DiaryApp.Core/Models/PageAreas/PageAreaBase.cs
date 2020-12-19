using DiaryApp.Core.Interfaces;
using DiaryApp.Core.Models.Pages;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Models.PageAreas
{
    public interface IMonthPageArea<T> : IPageArea<T, MonthPage>     
        where T : PageAreaBase<MonthPage>
    {

    }

    public abstract class PageAreaBase<TPage> : BaseEntity, IPageArea
        where TPage : PageBase
    {
        [Required]
        [MaxLength(100)]
        public string Header { get; set; } = string.Empty;
        [Required]
        public int PageId { get; set; }
        public virtual TPage Page { get; set; }

        public PageAreaBase()
        {

        }

        public PageAreaBase(TPage page, string header, bool withInitialization)
        {
            Page = page;
            Header = header;

            if (withInitialization)
                Initialize();
        }

        protected abstract void Initialize();
    }
}
