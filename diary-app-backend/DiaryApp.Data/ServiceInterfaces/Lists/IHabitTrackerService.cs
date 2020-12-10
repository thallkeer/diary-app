using DiaryApp.Core;
using DiaryApp.Core.DTO;

namespace DiaryApp.Data.ServiceInterfaces
{
    public interface IHabitTrackerService : ICrudService<HabitTrackerDto, HabitTracker>
    {
    }
}
