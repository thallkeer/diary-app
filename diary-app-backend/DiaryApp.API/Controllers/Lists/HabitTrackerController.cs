using AutoMapper;
using DiaryApp.API.Controllers.Lists;
using DiaryApp.Core;
using DiaryApp.Core.DTO;
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