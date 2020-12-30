using DiaryApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Models.PageAreas
{
    public interface IMonthPageArea<T> : IPageArea<T, MonthPage>     
        where T : MonthPageArea
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

        public override bool Equals(object obj)
        {
            return obj is PageAreaBase<TPage> @base &&
                   Id == @base.Id &&
                   Header == @base.Header &&
                   PageId == @base.PageId &&
                   EqualityComparer<TPage>.Default.Equals(Page, @base.Page);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Header, PageId, Page);
        }
    }
}
