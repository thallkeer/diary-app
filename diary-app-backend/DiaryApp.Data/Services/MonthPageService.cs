using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiaryApp.Core;

namespace DiaryApp.Data.Services
{
    public class MonthPageService : PageService<MonthPage>, IMonthPageService
    {
        public MonthPageService(ApplicationContext context) : base(context)
        {
        }

        public async Task<T> GetPageArea<T>(int pageID) where T : PageAreaBase
        {
            return await context.Set<T>().FirstOrDefaultAsync(area => area.PageID == pageID);            
        }
    }
}
