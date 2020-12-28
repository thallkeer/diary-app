using DiaryApp.Core.Models.PageAreas;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DiaryApp.Core.Models
{
    public class DiaryAreaList<TList, TListItem, TArea, TPage> : BaseEntity
        where TList : DiaryList<TListItem>, new()
        where TListItem : ListItemBase
        where TArea : PageAreaBase<TPage>
        where TPage : PageBase
    {
        [Required]
        public int ListID { get; set; }
        public virtual TList List { get; set; }
        [Required]
        public int AreaOwnerID { get; set; }
        public virtual TArea AreaOwner { get; set; }
        [NotMapped]
        public List<TListItem> Items { get => List.Items; set => List.Items = value; }

        public DiaryAreaList()
        {

        }

        public DiaryAreaList(string title)
        {
            List = new TList
            {
                Title = title
            };
        }

        /// <summary>
        /// Creates deep copy of wrapped list items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TListItem> CopyItems()
        {
            return List.Items.Select(i => (TListItem) i.GetCopy());
        }
    }
}