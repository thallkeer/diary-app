using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DiaryApp.Core.Models
{
    public class ListBase<T> : BaseEntity
        where T : ListItemBase
    {
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        [NotNull]
        public virtual List<T> Items { get; set; } = new List<T>();

        public ListBase()
        {
        }

        public ListBase(string title)
        {
            Title = title;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
