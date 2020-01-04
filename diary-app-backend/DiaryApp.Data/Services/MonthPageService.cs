using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiaryApp.Core;

namespace DiaryApp.Data.Services
{
    public class MonthPageService : CrudService<MonthPage>, IMonthPageService
    {
        public MonthPageService(ApplicationContext context) : base(context)
        {
        }

        public async Task<MonthPage> GetMonthPageForUser(string userID, int year, int month)
        {
            var page = await dbSet.FirstOrDefaultAsync
                (p => p.User.Id == userID && p.Year == year && p.Month == month);
            return page;
        }
    }
}
