using AutoMapper;
using DiaryApp.Core.Entities;
using DiaryApp.Models.DTO;
using DiaryApp.Services.DataInterfaces;
using Microsoft.Extensions.Logging;

namespace DiaryApp.API.Controllers
{
    public class HabitTrackersController : CrudController<HabitTrackerDto, HabitTracker>
    {
        public HabitTrackersController(ICrudService<HabitTrackerDto, HabitTracker> habitTrackerService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(habitTrackerService, mapper, loggerFactory)
        {
        }
    }
}