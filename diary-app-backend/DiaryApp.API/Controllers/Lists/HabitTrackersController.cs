using DiaryApp.Core.Entities;
using DiaryApp.Core.Entities.DiaryLists;
using DiaryApp.Services.DataInterfaces;
using DiaryApp.Services.DTO;

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