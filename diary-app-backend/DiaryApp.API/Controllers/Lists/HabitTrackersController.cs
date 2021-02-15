using AutoMapper;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DTO;
using DiaryApp.Services.DataInterfaces;

namespace DiaryApp.API.Controllers
{
    public class HabitTrackersController : CrudController<HabitTrackerDto, HabitTracker>
    {
        public HabitTrackersController(ICrudService<HabitTrackerDto, HabitTracker> habitTrackerService, IMapper mapper)
            : base(habitTrackerService, mapper)
        {
        }
    }
}