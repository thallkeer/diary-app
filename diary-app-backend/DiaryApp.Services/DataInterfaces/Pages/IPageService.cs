using DiaryApp.Core.Interfaces;
using System.Threading.Tasks;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DTO;

namespace DiaryApp.Services.DataInterfaces
{
    public interface IPageService<TPageDto, TPage> : IGetable<TPage>
        where TPageDto : PageDto
        where TPage : PageBase
    {
        /// <summary>
        /// Creates new page by given parameters if it's not exists and initialize it's page areas.
        /// </summary>
        /// <param name="userID">User id</param>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <returns></returns>
        Task<TPageDto> CreateAsync(int userID, int year, int month);

        /// <summary>
        /// Returns page area of defined type
        /// </summary>
        /// <typeparam name="TPageAreaDto">Page area dto type</typeparam>
        /// <typeparam name="TPageArea">Page area type</typeparam>
        /// <param name="pageID">Page identifier</param>
        /// <returns></returns>
        Task<TPageArea> GetPageArea<TPageArea>(int pageID) where TPageArea : class, IPageArea;

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
