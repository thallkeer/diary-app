using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DiaryApp.Core.Entities.DiaryLists;
using DiaryApp.Core.Entities.PageAreas;
using DiaryApp.Core.Entities.Pages;

namespace DiaryApp.Core.Entities.ListWrappers
{
    public abstract class DiaryAreaList<TList, TListItem, TArea, TPage> : BaseEntity
        where TList : DiaryList<TListItem>, new()
        where TListItem : DiaryListItem
        where TArea : PageAreaBase<TPage>
        where TPage : PageBase
    {
        public DiaryAreaList()
        { }

        /// <summary>
        /// Creates new instance of diary area list and initialize wrapped diary list
        /// </summary>
        /// <param name="diaryListTitle"></param>
        public DiaryAreaList(string diaryListTitle)
        {
            List = new TList
            {
                Title = diaryListTitle
            };
        }

        [Required]
        public int ListID { get; set; }

        public virtual TList List { get; set; }

        [Required]
        public int AreaOwnerId { get; set; }

        public virtual TArea AreaOwner { get; set; }

        [NotMapped]
        public IReadOnlyList<TListItem> Items => List.Items;

        public void Add(TListItem item) => List.Items.Add(item);

        public void AddRange(IEnumerable<TListItem> items) => List.Items.AddRange(items);

        /// <summary>
        /// Creates deep copy of wrapped list items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TListItem> CopyItems()
        {
            return Items.Select(i => (TListItem)i.GetCopy());
        }
    }
}