using DiaryApp.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DiaryApp.Data.Services
{
    public class PageService<T> : CrudService<T>, IPageService<T>
        where T : PageBase
    {
        public PageService(ApplicationContext context) : base(context)
        {
        }

        public async Task<T> GetPageForUser(int userID, int year, int month)
        {
            var page = await this.dbSet.FirstOrDefaultAsync
               (mp => mp.User.ID == userID && mp.Month == month && mp.Year == year);
            return page;
        }
    }
}
