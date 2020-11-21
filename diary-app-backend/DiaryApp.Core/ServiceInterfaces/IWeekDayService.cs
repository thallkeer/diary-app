using DiaryApp.Core.Models;
using DiaryApp.Core.Models.Pages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiaryApp.Core.ServiceInterfaces
{
    public interface IWeekDayService : ICrudService<WeekDay>
    {
        Task<IEnumerable<WeekDay>> GetByWeekPageAsync(WeekPage page);
    }
}
