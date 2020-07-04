using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DiaryApp.Core.Models.Lists
{
    public class ListBase<T>
        where T : ListItemBase
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        [Required]
        public int PageID { get; set; }
        /// <summary>
        /// Page where this list is placed on
        /// </summary>
        public virtual PageBase Page { get; set; }
        [NotNull]
        public virtual List<T> Items { get; set; } = new List<T>();

        public ListBase()
        {
        }

        public ListBase(string title, PageBase page)
        {
            Title = title;
            Page = page;
        }

        public override string ToString()
        {
            return $"{Title} {Page?.Year} {Page?.Month}";
        }
    }

    public static class ListBaseExtensions
    {
        /// <summary>
        /// Creates full deep copy of list
        /// </summary>
        /// <param name="page">New page-owner for list copy</param>
        /// <returns></returns>
        public static TList CreateDeepCopy<TList, TItem>(this TList original, PageBase page)
            where TList : ListBase<TItem>, new()
            where TItem : ListItemBase
        {
            var list = new TList()
            {
                Title = original.Title,
                Page = page,
                Items = new List<TItem>(original.Items.Count)
            };            

            original.Items.ForEach(item =>
            {
                list.Items.Add((TItem) item.GetCopy());
            });
            return list;
        }
    }
}
