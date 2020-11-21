using DiaryApp.Core;
using DiaryApp.Core.Models;
using DiaryApp.Core.Models.Pages;
using DiaryApp.Core.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiaryApp.Data.Services
{
    public class WeekDayService : CrudService<WeekDay>, IWeekDayService
    {
        private readonly IEventService eventService;
        public WeekDayService(ApplicationContext context) : base(context)
        {
            eventService = new EventService(context);
        }

        public async Task<IEnumerable<WeekDay>> GetByWeekPageAsync(WeekPage page)
        {
            Dictionary<DateTime, WeekDay> weekDays = await dbSet.Where(wd => wd.WeekPlansArea == page.WeekPlansArea)
                                      .ToDictionaryAsync(k => k.Day.Date, v => v);

            var eventsByWeekDay = eventService.GetItems()
                        .Where(ev => weekDays.ContainsKey(ev.Date.Date))
                        .GroupBy(k => k.Date.Date, v => v);

            await eventsByWeekDay.ForEachAsync(group =>
                weekDays[group.Key].Events = new List<EventItem>(group.Select(g => g)));

            return weekDays.Values;
        }
    }
}
