using DiaryApp.Core.Extensions;
using DiaryApp.Core.Models.PageAreas;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryApp.Core.Models
{
    public class DiaryListWrapper<TList, TListItem, TArea, TPage> : BaseEntity
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

        public DiaryListWrapper()
        {

        }

        public DiaryListWrapper(string title)
        {
            List = new TList
            {
                Title = title
            };
        }

        public List<TWrapper> GetCopyyy<TWrapper>(List<TWrapper> source) where TWrapper : DiaryListWrapper<TList, TListItem, TArea, TPage>, new()
        {
            var target = new List<TWrapper>(source.Count);
            source.ForEach(listWrapper =>
            {
                target.Add(new TWrapper
                {
                    List = listWrapper.List.CreateDeepCopy<TList, TListItem>()
                });
            });
            return target;
        }
    }
}