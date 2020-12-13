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
        Task<TPageDto> CreateAsync(int userID, int year, int month);
        Task<TPageDto> GetPageAsync(int userID, int year, int month);        
    }
}
