using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.Entities;
using DiaryApp.Models.DTO;
using DiaryApp.Data.DataInterfaces;

namespace DiaryApp.Data.Services
{
    public class MainPageService : PageService<MainPageDto, MainPage>, IMainPageService
    {
        public MainPageService(ApplicationContext context, IMapper mapper, IUserService userService) : base(context, mapper, userService)
        {
        }
    }
}
