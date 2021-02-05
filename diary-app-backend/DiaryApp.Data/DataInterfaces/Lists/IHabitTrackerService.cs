using DiaryApp.Core.Entities;
using DiaryApp.Models.DTO;

namespace DiaryApp.Data.DataInterfaces
{
    public interface IHabitTrackerService : ICrudService<HabitTrackerDto, HabitTracker>
    {
    }
}
