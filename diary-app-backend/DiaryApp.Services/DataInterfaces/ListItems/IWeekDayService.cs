using DiaryApp.Services.DTO;
using DiaryApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiaryApp.Services.DataInterfaces.ListItems
{
    public interface IWeekDayService : ICrudService<WeekDayDto, WeekDay>
    {
        Task<IEnumerable<WeekDayDto>> GetByWeekPageAsync(WeekPageDto page);
    }
}
