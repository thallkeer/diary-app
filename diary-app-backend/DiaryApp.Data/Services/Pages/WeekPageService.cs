using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Data.DTO;
using DiaryApp.Core.Models.Pages;
using DiaryApp.Data.ServiceInterfaces;
using DiaryApp.Data.ServiceInterfaces.ServiceInterfaces;

namespace DiaryApp.Data.Services
{
    public class WeekPageService : PageService<WeekPageDto, WeekPage>, IWeekPageService
    {
        private readonly IWeekDayService weekDayService;
        private readonly IEventListService eventService;
        public WeekPageService(ApplicationContext context, IEventListService eventService, IWeekDayService weekDayService, IMapper mapper, IUserService userService) : base(context, mapper, userService)
        {
            this.eventService = eventService;
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
