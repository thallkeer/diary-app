using AutoMapper;
using DiaryApp.Core.Entities;
using DiaryApp.Models.DTO;
using DiaryApp.Data.DataInterfaces;
using Microsoft.Extensions.Logging;

namespace DiaryApp.API.Controllers.ListItems
{
    public class HabitDaysController : CrudController<HabitDayDto, HabitDay>
    {

        public HabitDaysController(ICrudService<HabitDayDto, HabitDay> habitDayService
            , IMapper mapper, ILoggerFactory loggerFactory) : base(habitDayService, mapper, loggerFactory)
        {
        }
    }
}
