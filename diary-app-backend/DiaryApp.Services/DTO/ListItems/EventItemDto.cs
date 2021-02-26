using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryApp.Services.DTO
{
    public class EventItemDto : ListItemDto
    {
        [Required]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
    }
}
