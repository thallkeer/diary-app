using AutoMapper;
using DiaryApp.Core.Models;
using DiaryApp.Data.DTO;
using DiaryApp.Data.ServiceInterfaces;
using Microsoft.Extensions.Logging;

namespace DiaryApp.API.Controllers
{
    public class HabitTrackerController : CrudController<HabitTrackerDto, HabitTracker>
    {
        public HabitTrackerController(IHabitTrackerService habitTrackerService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(habitTrackerService, mapper, loggerFactory)
        {
        }
    }
}