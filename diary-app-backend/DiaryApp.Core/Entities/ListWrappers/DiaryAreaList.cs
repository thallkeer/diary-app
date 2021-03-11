using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DiaryApp.Core.Entities
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
        public int AreaOwnerID { get; set; }

        public virtual TArea AreaOwner { get; set; }

        [NotMapped]
        public List<TListItem> Items => List?.Items ?? new();

        /// <summary>
        /// Creates deep copy of wrapped list items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TListItem> CopyItems()
        {
            return Items.Select(i => (TListItem) i.GetCopy());
        }
    }
}