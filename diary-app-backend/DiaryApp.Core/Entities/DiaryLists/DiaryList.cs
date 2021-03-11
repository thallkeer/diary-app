using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Entities
{
    public class DiaryList<T> : BaseEntity where T : DiaryListItem
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

        public TList GetListCopy<TList>()
            where TList : DiaryList<T>, new()
        {
            var list = new TList()
            {
                Title = Title,
                Items = new List<T>(Items.Count)
            };

            Items.ForEach(item =>
            {
                list.Items.Add((T)item.GetCopy());
            });
            return list;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}