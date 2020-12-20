using DiaryApp.Core.Interfaces;
using DiaryApp.Core.Models;
using DiaryApp.Core.Models.PageAreas;
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
            where TList : DiaryList<TItem>, new()
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

        public static List<TWrapper> GetCopy<TWrapper, TList, TItem, TArea, TPage>(this List<TWrapper> source)
            where TWrapper : DiaryAreaList<TList, TItem, TArea, TPage>, new()
            where TList : DiaryList<TItem>, new()
            where TItem : ListItemBase
            where TArea : PageAreaBase<TPage>
            where TPage : PageBase
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

        public static List<PurchaseList> CopyPurchaseLists(this List<PurchaseList> source)
        {
            return source.GetCopy<PurchaseList, TodoList, TodoItem, PurchasesArea, MonthPage>();
        }
    }
}
