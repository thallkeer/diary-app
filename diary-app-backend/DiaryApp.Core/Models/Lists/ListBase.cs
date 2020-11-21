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

    public interface ICommonListWrapper : IListWrapper<CommonList, ListItem>
    { }

    public interface ITodoListWrapper : IListWrapper<TodoList, TodoItem> { }

    public interface IListWrapper<T,U>
        where T : ListBase<U>, new ()
        where U : ListItemBase
    {
        T List { get; set; }
        List<U> Items { get; set; }
    }

    public static class ListBaseExtensions
    {
        /// <summary>
        /// Creates full deep copy of list
        /// </summary>
        /// <param name="page">New page-owner for list copy</param>
        /// <returns></returns>
        public static TList CreateDeepCopy<TList, TItem>(this TList original)
            where TList : ListBase<TItem>, new()
            where TItem : ListItemBase
        {
            var list = new TList()
            {
                Title = original.Title,
                Items = new List<TItem>(original.Items.Count)
            };            

            original.Items.ForEach(item =>
            {
                list.Items.Add((TItem) item.GetCopy());
            });
            return list;
        }

        public static List<TWrapper> GetCopy<TWrapper, TList, TItem>(this List<TWrapper> source)
            where TWrapper : IListWrapper<TList, TItem>, new()
            where TList : ListBase<TItem>, new()
            where TItem : ListItemBase
        {
            var target = new List<TWrapper>(source.Count);
            source.ForEach(listWrapper =>
            {
                target.Add(new TWrapper
                {
                    List = listWrapper.List.CreateDeepCopy<TList,TItem>()
                });
            });
            return target;
        }
    }
}
