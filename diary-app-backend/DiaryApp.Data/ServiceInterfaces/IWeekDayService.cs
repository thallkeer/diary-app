using DiaryApp.Data.DTO;
using DiaryApp.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiaryApp.Data.ServiceInterfaces.ServiceInterfaces
{
    public interface IWeekDayService : ICrudService<WeekDayDto, WeekDay>
    {
        Task<IEnumerable<WeekDayDto>> GetByWeekPageAsync(WeekPageDto page);
    }
}
