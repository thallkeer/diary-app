using DiaryApp.Core;

namespace DiaryApp.Data.Services
{
    public class HabitTrackerService : CrudService<HabitsTracker>, IHabitTrackerService
    {
        public HabitTrackerService(ApplicationContext context) : base(context)
        {
        }
    }
}
