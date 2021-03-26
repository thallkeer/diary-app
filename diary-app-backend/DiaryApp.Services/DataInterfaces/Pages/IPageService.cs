using System.Threading.Tasks;
using DiaryApp.Core.Entities.PageAreas;
using DiaryApp.Core.Entities.Pages;
using DiaryApp.Services.DTO;
using DiaryApp.Services.DTO.PageAreas;

namespace DiaryApp.Services.DataInterfaces.Pages
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
        Task<TPageDto> CreateAsync(int userId, int year, int month);

        /// <summary>
        /// Returns page area of defined type or throws an exception if area is not exists
        /// </summary>
        /// <typeparam name="TPageArea">Page area type</typeparam>
        /// <typeparam name="TPageAreaDto">Page area dto type</typeparam>
        /// <param name="pageId">Page identifier</param>
        Task<TPageAreaDto> GetPageAreaOrThrowAsync<TPageArea, TPageAreaDto>(int pageId) 
            where TPageArea : PageAreaBase<TPage>
            where TPageAreaDto : PageAreaDto;

        /// <summary>
        /// Returns page by given page params or null if it's not exists.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        Task<TPageDto> GetPageAsync(int userId, int year, int month);
    }
}
