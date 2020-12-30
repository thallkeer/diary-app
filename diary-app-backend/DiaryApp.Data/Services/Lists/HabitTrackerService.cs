using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.Models;
using DiaryApp.Data.DTO;
using DiaryApp.Data.ServiceInterfaces;

namespace DiaryApp.Data.Services
{
    public class HabitTrackerService : CrudService<HabitTrackerDto, HabitTracker>, IHabitTrackerService
    {
        public HabitTrackerService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
