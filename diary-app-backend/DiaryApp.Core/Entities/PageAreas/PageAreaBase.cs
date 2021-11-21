using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DiaryApp.Core.Entities.Pages;
using DiaryApp.Core.Interfaces;

namespace DiaryApp.Core.Entities.PageAreas
{
    /// <summary>
    /// Represents base class for an area of a certain diary page.
    /// </summary>
    /// <typeparam name="TPage"></typeparam>
    public abstract class PageAreaBase<TPage> : BaseEntity, IPageArea
        where TPage : PageBase
    {
        public PageAreaBase()
        {

        }

        public PageAreaBase(TPage page, string header, bool withInitialization)
        {
            Guard.Against.Null(page, nameof(page));
            Guard.Against.NullOrEmpty(header, nameof(header));
            Page = page;
            PageId = page.Id;
            Header = header;

            if (withInitialization)
                Initialize();
        }

        /// <summary>
        /// Title of an area.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Header { get; set; } = string.Empty;

        /// <summary>
        /// Id of page owns this area.
        /// </summary>
        [Required]
        public int PageId { get; set; }

        /// <summary>
        /// Page owns this area.
        /// </summary>
        public virtual TPage Page { get; set; }

        /// <summary>
        /// Actions to do when area is created.
        /// </summary>
        protected abstract void Initialize();
    }
}
