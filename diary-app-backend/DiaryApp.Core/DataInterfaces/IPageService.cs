using System.Threading.Tasks;

namespace DiaryApp.Core
{
    public interface IPageService<T> : ICrudService<T> where T : PageBase
    {
        Task<T> GetPageForUser(string userID, int year, int month);
    }
}
