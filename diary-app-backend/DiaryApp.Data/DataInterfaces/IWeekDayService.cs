﻿using DiaryApp.Models.DTO;
using DiaryApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiaryApp.Data.DataInterfaces
{
    public interface IWeekDayService : ICrudService<WeekDayDto, WeekDay>
    {
        Task<IEnumerable<WeekDayDto>> GetByWeekPageAsync(WeekPageDto page);
    }
}