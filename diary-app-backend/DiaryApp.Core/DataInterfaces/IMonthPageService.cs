using System.Threading.Tasks;

namespace DiaryApp.Core
{
    public interface IMonthPageService : ICrudService<MonthPage>
    {
        Task<T> GetPageArea<T>(int pageID) where T : PageAreaBase;  
        Task<MonthPage> GetMonthPageForUser(string userID, int year, int month);
    }
}
