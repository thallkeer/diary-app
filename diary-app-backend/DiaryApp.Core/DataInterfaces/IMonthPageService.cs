using System.Threading.Tasks;

namespace DiaryApp.Core
{
    public interface IMonthPageService : IPageService<MonthPage>
    {
        Task<T> GetPageArea<T>(int pageID) where T : PageAreaBase;
    }
}
