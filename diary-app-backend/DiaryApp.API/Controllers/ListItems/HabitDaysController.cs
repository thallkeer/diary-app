using DiaryApp.Core.Entities;
using DiaryApp.Services.DTO;
using DiaryApp.Services.DataInterfaces;

namespace DiaryApp.API.Controllers.ListItems
{
    public class HabitDaysController : CrudController<HabitDayDto, HabitDay>
    {

        public HabitDaysController(ICrudService<HabitDayDto, HabitDay> habitDayService
            ) : base(habitDayService)
        {
        }
    }
}
