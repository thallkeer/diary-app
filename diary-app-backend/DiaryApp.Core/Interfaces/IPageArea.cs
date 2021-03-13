using DiaryApp.Core.Entities;
using DiaryApp.Core.Entities.PageAreas;
using DiaryApp.Core.Entities.Pages;

namespace DiaryApp.Core.Interfaces
{
    public interface IPageArea
    {
        int PageId { get; set; }
    }

    public interface IMonthPageArea<in T> where T : MonthPageArea
    {
        /// <summary>
        /// Copy content from other area
        /// </summary>
        /// <param name="other"></param>
        void AddDataFromOtherArea(T other);
    }
}
