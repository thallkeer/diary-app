using AutoMapper;
using DiaryApp.Core.Models;
using DiaryApp.Data.DTO;
using DiaryApp.Data.ServiceInterfaces;
using Microsoft.Extensions.Logging;

namespace DiaryApp.API.Controllers
{
    public class HabitTrackersController : CrudController<HabitTrackerDto, HabitTracker>
    {
        public HabitTrackersController(IHabitTrackerService habitTrackerService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(habitTrackerService, mapper, loggerFactory)
        {
        }
    }
}