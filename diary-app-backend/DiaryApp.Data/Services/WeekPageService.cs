using DiaryApp.Core;
using DiaryApp.Core.Models;
using DiaryApp.Core.Models.Pages;
using DiaryApp.Core.ServiceInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiaryApp.Data.Services
{
    public class WeekPageService : PageService<WeekPage>,IWeekPageService
    {
        private readonly IWeekDayService weekDayService;
        private readonly IEventService eventService;
        public WeekPageService(ApplicationContext context) : base(context)
        {
            eventService = new EventService(context);
            weekDayService = new WeekDayService(context);
        }

        public async override Task<WeekPage> GetPageForUser(int userID, int year, int month)
        {
            WeekPage page = await base.GetPageForUser(userID, year, month);
            if (page == null)
                return null;
            //List<DateTime> weekDates = new List<DateTime>(7);
            //foreach (DayOfWeek dayOfWeek in Enum.GetValues(typeof(DayOfWeek)))
            //{
            //    DateTime day = ISOWeek.ToDateTime(year, page.WeekPlansArea.WeekNumber, dayOfWeek);
            //    weekDates.Add(day);
            //}
            var weekDays = await weekDayService.GetByWeekPageAsync(page);
            page.WeekPlansArea.WeekDays = new List<WeekDay>(weekDays);
            return page;
        }

        public async override Task<WeekPage> CreatePageByParams(int userID, int year, int month)
        {           
            var weekPage = await base.CreatePageByParams(userID, year, month);

            await Update(weekPage);

            return weekPage;
        }
    }
}
