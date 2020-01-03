using DiaryApp.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DiaryApp.Data.Services
{
    public class MainPageService : CrudService<MainPage>, IMainPageService
    {
        public MainPageService(ApplicationContext context) : base(context)
        {
        }

        public async Task<MainPage> GetMainPageForUser(string userID, int year, int month)
        {
            var mainPage = await dbSet.FirstOrDefaultAsync
               (mp => mp.User.Id == userID && mp.Month == month && mp.Year == year);
            return mainPage;
        }
    }
}
