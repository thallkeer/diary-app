using DiaryApp.Core.DTO;
using DiaryApp.Core.Interfaces;
using System.Threading.Tasks;

namespace DiaryApp.Data.ServiceInterfaces
{
    public interface IPageService<TPageDto> where TPageDto : PageDto
    {
        /// <summary>
        /// Returns page area of defined type
        /// </summary>
        /// <typeparam name="TPageAreaDto">Page area dto type</typeparam>
        /// <typeparam name="TPageArea">Page area type</typeparam>
        /// <param name="pageID">Page identifier</param>
        /// <returns></returns>
        Task<TPageAreaDto> GetPageArea<TPageAreaDto, TPageArea>(int pageID)
            where TPageAreaDto : PageAreaDto
            where TPageArea : class, IPageArea;

        /// <summary>
        /// Creates new page by given parameters if it's not exists and initialize it's page areas.
        /// </summary>
        /// <param name="userID">User id</param>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <returns></returns>
        Task<TPageDto> CreateAsync(int userID, int year, int month);

        /// <summary>
        /// Creates new page by given parameters if it's not exists. Also initializes page areas, if needed.
        /// </summary>
        /// <param name="pageDto">Page dto</param>
        /// <param name="initializePageAreas">Whether need to initialize page arees</param>
        /// <returns></returns>
        Task<TPageDto> CreateAsync(PageDto pageDto, bool initializePageAreas);

        /// <summary>
        /// Returns page by given page params or null if it's not exists.
        /// </summary>
        /// <param name="userID">User id</param>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <returns></returns>
        Task<TPageDto> GetPageAsync(int userID, int year, int month);        
    }
}
