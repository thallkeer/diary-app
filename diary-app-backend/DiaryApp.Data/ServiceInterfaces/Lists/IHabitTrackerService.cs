using DiaryApp.Core.Models;
using DiaryApp.Data.DTO;

namespace DiaryApp.Data.ServiceInterfaces
{
    public interface IHabitTrackerService : ICrudService<HabitTrackerDto, HabitTracker>
    {
    }
}
