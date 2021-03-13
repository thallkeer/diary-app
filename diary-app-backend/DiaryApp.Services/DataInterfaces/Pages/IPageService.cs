using DiaryApp.Core.Interfaces;
using System.Threading.Tasks;
using DiaryApp.Core.Entities;
using DiaryApp.Core.Entities.Pages;
using DiaryApp.Services.DTO;

namespace DiaryApp.Services.DataInterfaces
{
    public interface IPageService<TPageDto, TPage> : IGetable<TPageDto, TPage>
        where TPageDto : PageDto
        where TPage : PageBase
    {
        /// <summary>
        /// Creates new page by given parameters if it's not exists and initialize it's page areas.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <returns></returns>
        Task<TPageDto> CreateAsync(int userId, int year, int month);

        /// <summary>
        /// Returns page area of defined type or throws an exception if area is not exists
        /// </summary>
        /// <typeparam name="TPageArea">Page area type</typeparam>
        /// <param name="pageId">Page identifier</param>
        /// <returns></returns>
        Task<TPageArea> GetPageAreaOrThrowAsync<TPageArea>(int pageId) where TPageArea : class, IPageArea;

        /// <summary>
        /// Returns page by given page params or null if it's not exists.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <returns></returns>
        Task<TPageDto> GetPageAsync(int userId, int year, int month);
    }
}
