using System.Threading.Tasks;

namespace DiaryApp.Core
{
    public interface IPageService<T> : ICrudService<T> where T : PageBase
    {
        Task<T> GetPageForUser(int userID, int year, int month);
        Task<T> CreatePageByParams(int userID, int year, int month);
    }
}
