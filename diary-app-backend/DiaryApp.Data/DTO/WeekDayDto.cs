using System;
using System.Collections.Generic;

namespace DiaryApp.Data.DTO
{
    public class WeekDayDto : BaseDto
    {
        public DateTime Day { get; set; }
        public string Subject { get; set; }
        public int WeekPlansAreaID { get; set; }
        /// <summary>
        /// Events from important event list
        /// </summary>
        public List<EventItemDto> Events { get; set; } = new List<EventItemDto>();
    }
}
