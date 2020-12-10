using DiaryApp.Core.Interfaces;
using DiaryApp.Core.Models;
using System.Collections.Generic;

namespace DiaryApp.Core.Extensions
{
    public static class ListBaseExtensions
    {
        /// <summary>
        /// Creates full deep copy of list
        /// </summary>
        /// <typeparam name="TList">Type of original list</typeparam>
        /// <typeparam name="TItem">Type of item in list</typeparam>
        /// <param name="original">Original list</param>
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
                list.Items.Add((TItem)item.GetCopy());
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
                    List = listWrapper.List.CreateDeepCopy<TList, TItem>()
                });
            });
            return target;
        }
    }
}
