using DiaryApp.Core;

namespace DiaryApp.Data.Services
{
    public class MainPageService : PageService<MainPage>, IMainPageService
    {
        public MainPageService(ApplicationContext context) : base(context)
        {
        }
    }
}
