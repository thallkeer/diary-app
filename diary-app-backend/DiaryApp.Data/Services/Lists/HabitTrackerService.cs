using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.Entities;
using DiaryApp.Models.DTO;
using DiaryApp.Data.DataInterfaces;

namespace DiaryApp.Data.Services
{
    public class HabitTrackerService : CrudService<HabitTrackerDto, HabitTracker>, IHabitTrackerService
    {
        public HabitTrackerService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
