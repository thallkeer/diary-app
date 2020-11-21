using DiaryApp.Core.Models.PageAreas;

namespace DiaryApp.Core.Interfaces
{
    public enum PageAreaType
    {
        Purchases,
        Desires,
        Ideas,
        Goals,
        WeekPlans,
        Notes
    }

    public interface IPageArea<T,TPage>
        where T : PageAreaBase<TPage>
        where TPage : PageBase
    {
        /// <summary>
        /// Page
        /// </summary>
        TPage Page { get; set; }
        /// <summary>
        /// Area type
        /// </summary>
        PageAreaType AreaType { get; }        
        /// <summary>
        /// Copy content from other area
        /// </summary>
        /// <param name="otherArea"></param>
        void AddFromOtherArea(T other);
        /// <summary>
        /// Transfer data to page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        T TransferAreaData(TPage page);
    }
}
