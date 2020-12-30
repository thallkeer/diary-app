using DiaryApp.Core;
using DiaryApp.Data.DTO;

namespace DiaryApp.Data.ServiceInterfaces
{
    public interface IHabitTrackerService : ICrudService<HabitTrackerDto, HabitTracker>
    {
    }
}
