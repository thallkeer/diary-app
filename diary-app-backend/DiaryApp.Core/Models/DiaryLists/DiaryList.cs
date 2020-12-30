using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Models
{
    public class DiaryList<T> : BaseEntity  where T : DiaryListItem
    {
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        public virtual List<T> Items { get; set; } = new List<T>();

        public DiaryList()
        {
        }

        public DiaryList(string title)
        {
            Title = title;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
