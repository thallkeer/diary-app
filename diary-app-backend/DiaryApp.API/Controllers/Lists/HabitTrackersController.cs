using DiaryApp.Core.Entities.DiaryLists;
using DiaryApp.Services.DataInterfaces;
using DiaryApp.Services.DTO.Lists;

namespace DiaryApp.API.Controllers
{
    public class HabitTrackersController : CrudController<HabitTrackerDto, HabitTracker>
    {
        public HabitTrackersController(ICrudService<HabitTrackerDto, HabitTracker> habitTrackerService)
            : base(habitTrackerService)
        {
        }
    }
}