using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using DiaryApp.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiaryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonthPageController : ControllerBase
    {
        private readonly IMonthPageService monthPageService;
        private readonly IMapper mapper;

        public MonthPageController(ApplicationContext context, IMapper mapper)
        {
            this.monthPageService = new MonthPageService(context);
            this.mapper = mapper;
        }

        [HttpGet("{userId}/{year}/{month}")]
        public async Task<MonthPageModel> GetMonthPage(string userId, int year, int month)
        {
            var monthPage = await monthPageService.GetMonthPageForUser(userId, year, month);

            var model = mapper.Map<MonthPageModel>(monthPage);

            model.DesiresLists = MapList<EventList, EventListModel>(monthPage.DesiresArea.DesiresLists);
            model.GoalsLists = MapList<HabitsTracker, HabitsTrackerModel>(monthPage.GoalsArea.GoalsLists);
            model.PurchasesLists = MapList<TodoList,TodoListModel>(monthPage.PurchasesArea.PurchasesLists);
            model.IdeasList = mapper.Map<EventListModel>(monthPage.IdeasArea.IdeasList);

            return model;
        }

        private List<U> MapList<T,U>(List<T> listToMap)
        {
            return listToMap.Select(l => mapper.Map<U>(l)).ToList();
        }
    }
}