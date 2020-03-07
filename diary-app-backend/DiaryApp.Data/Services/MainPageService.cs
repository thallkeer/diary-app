using DiaryApp.Core;
using System.Threading.Tasks;

namespace DiaryApp.Data.Services
{
    public class MainPageService : PageService<MainPage>, IMainPageService
    {
        private readonly IEventService eventService;
        private readonly ITodoService todoService;
        public MainPageService(ApplicationContext context) : base(context)
        {
            eventService = new EventService(context);
            todoService = new TodoService(context);
        }

        public async override Task<MainPage> CreatePageByParams(int userID, int year, int month)
        {
            var mainPage = new MainPage()
            {
                UserID = userID,
                Year = year,
                Month = month,
            };

            await Create(mainPage);

            var impEvents = new EventList()
            {
                Title = "Важные события",
                Page = mainPage
            };

            var todos = new TodoList
            {
                Title = "Важные дела",
                Page = mainPage
            };

            await eventService.Create(impEvents);
            await todoService.Create(todos);
            return mainPage;
        }
    }
}
