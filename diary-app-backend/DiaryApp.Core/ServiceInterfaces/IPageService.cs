using DiaryApp.Core.Models.PageAreas;
using System.Threading.Tasks;

namespace DiaryApp.Core
{
    public interface IPageService<T> : ICrudService<T> where T : PageBase
    {
        Task<TArea> GetPageArea<TArea>(int pageID) where TArea : PageAreaBase<T>;
        Task<T> GetPageForUser(int userID, int year, int month);
        Task<T> CreatePageByParams(int userID, int year, int month);
    }
}
