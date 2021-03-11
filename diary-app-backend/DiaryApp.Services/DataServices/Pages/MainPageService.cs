using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DTO;
using DiaryApp.Services.DataInterfaces;

namespace DiaryApp.Services.DataServices
{
    public class MainPageService : PageService<MainPageDto, MainPage>, IMainPageService
    {
        public MainPageService(ApplicationContext context, IMapper mapper, IUserService userService) : base(context, mapper, userService)
        {
        }
    }
}
