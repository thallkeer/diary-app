using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Entities.DiaryLists
{
    public abstract class DiaryList<T> : BaseEntity where T : DiaryListItem
    {
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        public virtual List<T> Items { get; set; } = new();

        public DiaryList()
        {
        }

        public DiaryList(string title)
        {
            Title = title;
        }
    }
}