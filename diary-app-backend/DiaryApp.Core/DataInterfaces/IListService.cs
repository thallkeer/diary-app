using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiaryApp.Core
{
    public interface IListService<TList, TItem> : ICrudService<TList> where TList : class
    {        
        TList GetByPageID(int pageID);
        Task AddItem(TItem eventItem, int ownerID);
        Task UpdateItem(TItem eventItem);
        Task DeleteItem(int itemID);
        Task<TItem> GetItemByID(int itemID);
        IEnumerable<TList> GetListsByPageID(int pageID);
    }
}
