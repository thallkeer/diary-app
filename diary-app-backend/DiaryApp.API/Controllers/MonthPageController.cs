using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using DiaryApp.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiaryApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MonthPageController : ControllerBase
    {
        private readonly IMonthPageService monthPageService;
        private readonly IMapper mapper;

        public MonthPageController(IMonthPageService monthPageService, IMapper mapper)
        {
            this.monthPageService = monthPageService;
            this.mapper = mapper;
        }

        [HttpGet("{userId}/{year}/{month}")]
        public async Task<MonthPageModel> GetMonthPage(string userId, int year, int month)
        {
            var monthPage = await monthPageService.GetPageForUser(userId, year, month);

            var model = mapper.Map<MonthPageModel>(monthPage);

            return model;
        }

        [HttpGet("purchasesArea/{pageID}")]
        public async Task<PurchasesAreaModel> GetPurchasesArea(int pageID)
        {
           return await GetPageArea<PurchasesArea, PurchasesAreaModel>(pageID);
        }

        [HttpGet("desiresArea/{pageID}")]
        public async Task<DesiresAreaModel> GetDesiresArea(int pageID)
        {
            return await GetPageArea<DesiresArea, DesiresAreaModel>(pageID);
        }

        [HttpGet("ideasArea/{pageID}")]
        public async Task<IdeasAreaModel> GetIdeasArea(int pageID)
        {
            return await GetPageArea<IdeasArea, IdeasAreaModel>(pageID);
        }

        [HttpGet("goalsArea/{pageID}")]
        public async Task<GoalsAreaModel> GetGoalsArea(int pageID)
        {
            return await GetPageArea<GoalsArea, GoalsAreaModel>(pageID);
        }

        private async Task<TDto> GetPageArea<TEntity,TDto>(int pageID) 
            where TDto : PageAreaModel 
            where TEntity : PageAreaBase
        {
            var area = await monthPageService.GetPageArea<TEntity>(pageID);
            return mapper.Map<TDto>(area);
        }
    }
}