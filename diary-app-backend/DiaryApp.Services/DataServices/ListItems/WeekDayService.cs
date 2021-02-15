using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Services.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DataInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace DiaryApp.Services.Services
{
    public class WeekDayService : CrudService<WeekDayDto, WeekDay>, IWeekDayService
    {
        public WeekDayService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public Task<IEnumerable<WeekDayDto>> GetByWeekPageAsync(WeekPageDto page)
        {
            throw new NotImplementedException();
            ///TODO: add week plans area id to dto
            //Dictionary<DateTime, WeekDay> weekDays = await dbSet/*Where(wd => wd.WeekPlansAreaID == page.)*/
            //                          .ToDictionaryAsync(k => k.Day.Date, v => v);

            //var eventsByWeekDay = (await eventService.GetItems())
            //            .Where(ev => weekDays.ContainsKey(ev.Date.Date))
            //            .GroupBy(k => k.Date.Date, v => v);

            //eventsByWeekDay.ToList().ForEach(group =>
            //    weekDays[group.Key].Events = new List<EventItemDto>(group.Select(g => g)));

            //return weekDays.Values;
        }
    }
}
