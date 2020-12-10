using System.Collections.Generic;

namespace DiaryApp.Core.DTO
{
    public class ListDto<T> : BaseDto where T : ListItemDto
    {
        public string Title { get; set; }
        public List<T> Items { get; set; }
    }
}
