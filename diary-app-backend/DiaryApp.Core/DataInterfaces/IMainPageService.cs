using System.Threading.Tasks;

namespace DiaryApp.Core
{
    public interface IMainPageService : ICrudService<MainPage>
    {
        Task<MainPage> GetMainPageForUser(string userID, int year, int month);
    }
}
