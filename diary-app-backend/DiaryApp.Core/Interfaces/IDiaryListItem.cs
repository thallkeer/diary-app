using DiaryApp.Core.Models;

namespace DiaryApp.Core.Interfaces
{
    public interface IDiaryListItem<TList, TItem> 
        where TList : DiaryList<TItem>
        where TItem : ListItemBase
    {
        /// <summary>
        /// List owner of this item
        /// </summary>
        TList Owner { get; set; }
    }
}
