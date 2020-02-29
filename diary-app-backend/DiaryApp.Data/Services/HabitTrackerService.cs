using DiaryApp.Core;
using System.Linq;

namespace DiaryApp.Data.Services
{
    public class HabitTrackerService : CrudService<HabitsTracker>, IHabitTrackerService
    {
        public HabitTrackerService(ApplicationContext context) : base(context)
        {
        }

        public override IQueryable<HabitsTracker> GetAll()
        {
            return base.GetAll().OrderBy(t => t.ID);
        }
    }
}
