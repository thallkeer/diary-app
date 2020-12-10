using DiaryApp.Core.DTO;
using DiaryApp.Core.Interfaces;
using System.Threading.Tasks;

namespace DiaryApp.Data.ServiceInterfaces
{
    public interface IPageService<TPageDto> where TPageDto : PageDto
    {
        Task<TPageDto> GetPageForUser(int userID, int year, int month);
        Task<TPageDto> CreatePageByParams(int userID, int year, int month);
        Task<TPageAreaDto> GetPageArea<TPageAreaDto, TPageArea>(int pageID)
            where TPageAreaDto : PageAreaDto
            where TPageArea : class, IPageArea;
    }
}
