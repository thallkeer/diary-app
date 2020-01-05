using System.Linq;
using System.Threading.Tasks;
using DiaryApp.Core;
using Microsoft.AspNetCore.Mvc;
using DiaryApp.API.Models;
using AutoMapper;
using DiaryApp.Data.Services;

namespace DiaryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainPageController : ControllerBase
    {
        private readonly IMainPageService mainPageService;
        private readonly IMapper mapper;

        public MainPageController(ApplicationContext context, IMapper mapper)
        {
            this.mainPageService = new MainPageService(context);
            this.mapper = mapper;
        }

        [HttpGet("{userId}/{year}/{month}")]
        public async Task<MainPageModel> GetMainPage(string userId,int year,int month)
        {
            var mainPage = await mainPageService.GetMainPageForUser(userId, year, month);

            var model = mapper.Map<MainPageModel>(mainPage);

            //model.ImportantEvents.Items = model.ImportantEvents.Items.OrderBy(e => e.Date).ToList();
            
            return model;
        }
    }
}