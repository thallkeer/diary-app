using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.Entities;
using DiaryApp.Models.DTO;
using DiaryApp.Services.DataInterfaces;

namespace DiaryApp.Services.Services
{
    public class WeekPageService : PageService<WeekPageDto, WeekPage>, IWeekPageService
    {
        private readonly IWeekDayService weekDayService;
        public WeekPageService(ApplicationContext context, IWeekDayService weekDayService, IMapper mapper, IUserService userService) : base(context, mapper, userService)
        {
            this.weekDayService = weekDayService;
        }

        //public async Task<WeekPage> GetPageAsync(int userID, int year, int month)
        //{
        //    WeekPage page = await base.GetPageAsync(userID, year, month);
        //    if (page == null)
        //        return null;
        //    //TODO: deal with code

        //    //List<DateTime> weekDates = new List<DateTime>(7);
        //    //foreach (DayOfWeek dayOfWeek in Enum.GetValues(typeof(DayOfWeek)))
        //    //{
        //    //    DateTime day = ISOWeek.ToDateTime(year, page.WeekPlansArea.WeekNumber, dayOfWeek);
        //    //    weekDates.Add(day);
        //    //}
        //    var weekDays = await weekDayService.GetByWeekPageAsync(page);
        //    //page.WeekPlansArea.WeekDays = new List<WeekDay>(weekDays);
        //    return page;
        //}
    }
}
