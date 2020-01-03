using System.Threading.Tasks;

namespace DiaryApp.Core
{
    public interface IMonthPageService : ICrudService<MonthPage>
    {
        Task<MonthPage> GetMonthPageForUser(string userID, int year, int month);
    }
}
