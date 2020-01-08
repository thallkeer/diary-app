using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using DiaryApp.Data.Services;
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

            //MonthPage monthPage = context.MonthPages.Find(2);

            //var beautyAndHealth = new TodoList
            //{
            //    Year = 2020,
            //    Month = 1,
            //    Title = "Красота и здоровье",
            //    Items = new List<TodoItem>
            //    {
            //        new TodoItem
            //        {
            //            Subject = "Карандаш для губ VS"
            //        },
            //        new TodoItem
            //        {
            //            Subject = "Снуп"
            //        }
            //    },
            //    Page = monthPage
            //};

            //var othersList = new TodoList
            //{
            //    Year = 2020,
            //    Month = 1,
            //    Title = "Другое",
            //    Items = new List<TodoItem>
            //    {
            //        new TodoItem
            //        {
            //            Subject = "Картриджи"
            //        }
            //    },
            //    Page = monthPage
            //};

            //context.TodoLists.AddRange(beautyAndHealth, othersList);

            //context.SaveChanges();

            //var purchasesArea = context.PurchasesAreas.Find(2);

            //purchasesArea.PurchasesLists.Add(beautyAndHealth);
            //purchasesArea.PurchasesLists.Add(othersList);

            //context.SaveChanges();

            //var desiresArea = context.DesiresAreas.Find(2);

            //var toRead = new EventList
            //{
            //    Month = 1,
            //    Year = 2020,
            //    Title = "Прочитать",
            //    Page = monthPage
            //};
            //var toWatch = new EventList
            //{
            //    Month = 1,
            //    Year = 2020,
            //    Title = "Посмотреть",
            //    Page = monthPage
            //};
            //var toVisit = new EventList
            //{
            //    Month = 1,
            //    Year = 2020,
            //    Title = "Посетить",
            //    Page = monthPage
            //};

            //context.EventLists.AddRange(toRead, toWatch, toVisit);

            //context.SaveChanges();

            //desiresArea.DesiresLists.AddRange(new EventList[]
            //{
            //    toRead, toWatch, toVisit
            //});

            //context.SaveChanges();
        }

        [HttpGet("{userId}/{year}/{month}")]
        public async Task<MonthPageModel> GetMonthPage(string userId, int year, int month)
        {
            var monthPage = await monthPageService.GetMonthPageForUser(userId, year, month);

            var model = mapper.Map<MonthPageModel>(monthPage);

            return model;
        }

        [HttpGet("/purchasesArea/{pageID}")]
        public async Task<PurchasesAreaModel> GetPurchasesArea(int pageID)
        {
            var area = await monthPageService.GetPageArea<PurchasesArea>(pageID);
            return mapper.Map<PurchasesAreaModel>(area);
        }

        private List<U> MapList<T,U>(List<T> listToMap)
        {
            return listToMap.Select(l => mapper.Map<U>(l)).ToList();
        }
    }
}