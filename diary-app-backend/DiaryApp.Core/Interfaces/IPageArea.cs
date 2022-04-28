using DiaryApp.Core.Entities.PageAreas;

namespace DiaryApp.Core.Interfaces
{
    public interface IMonthPageArea<in T> where T : MonthPageArea
    {
        /// <summary>
        /// Copy content from other area
        /// </summary>
        /// <param name="other"></param>
        void AddDataFromOtherArea(T other);
    }
}
