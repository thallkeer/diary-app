using System.Threading.Tasks;
using DiaryApp.Core;
using Microsoft.AspNetCore.Mvc;
using DiaryApp.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace DiaryApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MainPageController : ControllerBase
    {
        private readonly IMainPageService mainPageService;
        private readonly IMapper mapper;

        public MainPageController(IMainPageService mainPageService, IMapper mapper)
        {
            this.mainPageService = mainPageService;
            this.mapper = mapper;
        }

        [HttpGet("{userId}/{year}/{month}")]
        public async Task<MainPageModel> GetMainPage(string userId,int year,int month)
        {
            var mainPage = await mainPageService.GetPageForUser(userId, year, month);

            var model = mapper.Map<MainPageModel>(mainPage);

            //model.ImportantEvents.Items = model.ImportantEvents.Items.OrderBy(e => e.Date).ToList();
            
            return model;
        }
    }
}