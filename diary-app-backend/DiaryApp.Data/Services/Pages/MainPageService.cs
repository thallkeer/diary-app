using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Data.DTO;
using DiaryApp.Data.ServiceInterfaces;

namespace DiaryApp.Data.Services
{
    public class MainPageService : PageService<MainPageDto, MainPage>, IMainPageService
    {
        public MainPageService(ApplicationContext context, IMapper mapper, IUserService userService) : base(context, mapper, userService)
        {
        }
    }
}
