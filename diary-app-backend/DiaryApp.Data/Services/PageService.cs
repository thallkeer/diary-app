using DiaryApp.Core;
using DiaryApp.Core.Models.PageAreas;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DiaryApp.Data.Services
{
    public abstract class PageService<T> : CrudService<T>, IPageService<T>
        where T : PageBase, new()
    {
        public PageService(ApplicationContext context) : base(context)
        {
        }       

        public virtual async Task<T> GetPageForUser(int userID, int year, int month)
        {
            var page = await this.dbSet.FirstOrDefaultAsync
               (mp => mp.User.ID == userID && mp.Month == month && mp.Year == year);
            return page;
        }

        public virtual async Task<T> CreatePageByParams(int userID, int year, int month)
        {
            var page = new T()
            {
                UserID = userID,
                Year = year,
                Month = month
            };

            page.CreateAreas();

            await Create(page);           

            return page;
        }

        public async Task<TArea> GetPageArea<TArea>(int pageID) where TArea : PageAreaBase<T>
        {
            return await context.Set<TArea>().FirstOrDefaultAsync(area => area.Page.ID == pageID);
        }
    }
}
