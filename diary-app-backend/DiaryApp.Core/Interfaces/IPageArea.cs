using DiaryApp.Core.Entities;
using DiaryApp.Core.Entities.PageAreas;

namespace DiaryApp.Core.Interfaces
{
    public interface IPageArea
    {
        int PageId { get; set; }
    }

    public interface IPageArea<T, TPage> : IPageArea
        where T : PageAreaBase<TPage>
        where TPage : PageBase
    {
        /// <summary>
        /// Page
        /// </summary>
        TPage Page { get; set; }

        /// <summary>
        /// Copy content from other area
        /// </summary>
        /// <param name="otherArea"></param>
        void AddDataFromOtherArea(T other);
    }

    public interface IMonthPageArea<T> : IPageArea<T, MonthPage> where T : MonthPageArea
    {
    }
}
