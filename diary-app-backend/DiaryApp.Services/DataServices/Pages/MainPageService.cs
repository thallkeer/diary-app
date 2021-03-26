using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.Entities.Pages;
using DiaryApp.Services.DTO;
using DiaryApp.Services.DataInterfaces;
using DiaryApp.Services.DataInterfaces.Users;
using DiaryApp.Services.DataServices.Pages;

namespace DiaryApp.Services.DataServices
{
    public class MainPageService : PageService<MainPageDto, MainPage>, IMainPageService
    {
        public MainPageService(ApplicationContext context, IMapper mapper, IUserService userService) : base(context, mapper, userService)
        {
        }
    }
}
