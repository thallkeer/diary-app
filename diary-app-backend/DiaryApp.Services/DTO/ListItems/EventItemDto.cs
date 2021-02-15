using System;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Services.DTO
{
    public class EventItemDto : ListItemDto
    {
        [Required]
        public DateTime Date { get; set; }
    }
}
